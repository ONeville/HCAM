import { HTTP } from '../../api'
const URL_Name = 'CodeMappers'
const URL_Detail = 'CodeMapperDetails'

import MapperTag from './mapper.vue'

export default {
  name: 'code-mapping',
  components: {MapperTag}, 
  props: [],
  data () {
    return {
      dialog: false,
      valid: false,
      expanded:false,
      loader: null,
      drawerMapper: null,
      loading3: false,
      upTitle:'Mapping codes',
      subTitle:'Mapping Setter',
      title:'Accounting Mappings',
      headers: [
        { text: '_', value: 'name', sortable: false, align: 'center', width: '30px' },
        { text: 'Code', align: 'left', value: 'actionCode' },
        { text: 'Action Code', align: 'left', value: 'actionCodeName' },
        { text: 'Credit Account', value: 'creditAccountName', align: 'center', width: '220px' },
        { text: 'Debit Account', value: 'debitAccountName', align: 'center', width: '220px' },
        { text: 'Accounting Code', value: 'accountingCode', align: 'center' }
      ],
      items: [],
      itemDetails: [],
      errors: [],
      editedIndex: -1,
      editedItem: {
        id: 0,
        mapperName: '',
        isDefault: false,
        detailItems: []
      },
      defaultItem: {
        id: 0,
        mapperName: '',
        isDefault: false,
        detailItems: []
      },
      mapperSelected: null,
      accounts: [],
      creditSelected: null,
      debitSelect: null,
      drawerMap: {
        //height: 100%,
        margintop: '0px',
        width: '400px'
      }
    }
  },
  computed: { },
  watch: {
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 3000)

      if (l == 'loading3') {
        this.generateMapping()
      }
      
    },
    drawerMapper(newVal, oldVal) {
      //console.log('New: ' + newVal + '| Old: ' + oldVal)
      if (oldVal) {
        this.initialize()
      }
    }
  },

  created () {
    this.initialize()
    this.getMappings()
  },
  mounted () {

  },
  methods: {
    initialize () {
      this.mapperSelected = null
      HTTP.get(URL_Name + '/null')
        .then(response => {
          this.mapperSelected = response.data
          this.itemDetails = response.data.detailItems;
          this.getAccounts()          
        }).catch(e => { this.errors.push(e) })
    },

    getMappings() {
      HTTP.get(URL_Name)
        .then(response => {
          this.items = response.data
        }).catch(e => { this.errors.push(e) })
    },

    generateMapping() {
      if (this.mapperSelected && this.mapperSelected.id > 0) {
        HTTP.put(URL_Detail + '/' + this.mapperSelected.id)
          .then(response => {
            this.itemDetails = response.data
            this.loader = null
          }).catch(e => { this.errors.push(e) })
      }
    },

    getAccounts() {
      HTTP.get('Accounts')
        .then(response => {
          this.accounts = response.data
        }).catch(e => { this.errors.push(e) })
    },

    editItem (item) {
      this.$refs.form.reset()
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.mapperSelected = { id: item.accountTypeId, accountTypeName: item.accountTypeName}
      this.dialog = true
    },

    changedMapping(value) {
      //receive the value selected (return an array if is multiple)
      console.log('Selected: ' + value)
      this.mapperSelected = null
      HTTP.get(URL_Name + '/'+ value.id)
        .then(response => {
          this.mapperSelected = response.data
          this.itemDetails = response.data.detailItems;
          this.getAccounts()          
        }).catch(e => { this.errors.push(e) })
      
    },

    changedValue(value) {
      //receive the value selected (return an array if is multiple)
      console.log('Selected: ' + value)
      
    },
    saveEditItems() {
      //receive the label of the value selected (the label shown in the select, or an empty string)
      // console.log('Updated: ' + this.itemDetails)
      // var body = this.itemDetails[0]
      HTTP.put(URL_Detail + '/1', this.itemDetails)
          .then(response => { 
          //this.itemDetails = response.data
        })
          .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    },

    loadAccountTypes(){
      HTTP.get('AccountTypes')
          .then(response => { this.accountTypes = response.data })
          .catch(e => { this.errors.push(e) })
    },

    deleteItem (item) {
      var bobyParam = []
      
      item.EntityState = 8
      bobyParam.push(item)      
      HTTP.put(URL_Detail + '/1', bobyParam)
          .then(response => { 
            const index = this.itemDetails.indexOf(item)
            this.itemDetails.splice(index, 1)
        })
          .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    },

    close () {
      this.dialog = false
      this.$refs.form.reset()
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.mapperSelected = null
        this.valid = false;
      }, 300)
    },

    setDefault () {
      var bobyParam = {
        id: this.mapperSelected.id,
        mapperName: this.mapperSelected.mapperName,
        isDefault: true,
        detailItems: []
      }

      HTTP.put(URL_Name + '/' + bobyParam.id, bobyParam )
        .then(response => {
          this.initialize()
        })
      .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    },

    save () {
      if (this.$refs.form.validate()){
        this.editedItem.accountTypeId = this.mapperSelected.id
        this.editedItem.accountTypeName = this.mapperSelected.accountTypeName
        this.editedItem.AccountName = this.editedItem.accountName

        if (this.editedIndex > -1) {
          HTTP.put(URL_Name + '/' + this.editedItem.id, this.editedItem )
              .then(response => { this.initialize() })
              .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
        } else {
          HTTP.post(URL_Name, this.editedItem )
              .then(response => { this.initialize() })
              .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
        }
        this.close()

      }
    }
  }
}