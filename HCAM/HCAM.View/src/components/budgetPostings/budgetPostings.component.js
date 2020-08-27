import { HTTP } from '../../api'
const URL_Name = 'Postings'
const URL_Detail = 'PostingDetails'

import PostingView from './posting.vue'

export default {
  name: 'budget-postings',
  components: {PostingView}, 
  data () {
    return {
      drawerPosting: null,
      loading3: false,
      isPosted: false,
      upTitle:'Postings',
      subTitle:'Mapping Setter',
      title:'Budget Postings',
      headers: [
        { text: '_', value: 'name', sortable: false, align: 'center', width: '30px' },
        { text: 'ActionCode', align: 'left', value: 'description', width: '400px' },
        { text: 'Frequency', align: 'left', value: 'frequencyName', width: '90px' },
        { text: 'Eff. Date', align: 'left', value: 'effectiveDate', width: '150px' },
        { text: 'Budget', align: 'left', value: 'budgetAmount', width: '150px' },
        { text: 'Actual', align: 'left', value: 'actualAmount', width: '150px' },
        { text: 'Memo', align: 'left', value: 'Memo', width: '300px' }
      ],
      menuStart: false,
      menuEnd: false,
      menuEffDt: false,
      filterItem: {
        year: (new Date()).getFullYear(),
        startDate: null,
        endDate: null,
        Frequency: null,
      },
      items: [],
      errors: [],
      itemDetails:[],
      drawerStyle: {
        margintop: '0px',
        width: '390px'
      },
      colStyle: {
        color: 'green',
        background: 'red',
        width: '150px'
      },
      postingSelected: { },
      postingSelectedIndex: 0,
      subtitleStyle: {
        fontsize: '11px'
      }
    }
  },
  computed: { },
  watch: {
    drawerMapper(newVal, oldVal) {
      if (oldVal) {
        this.initialize()
      }
    }
  },

  created () {
    this.initialize()
  },
  mounted () {

  },
  methods: {
    initialize () {
      //this.mapperSelected = null
      HTTP.get(URL_Name)
        .then(response => {
          this.items = response.data      
        }).catch(e => { this.errors.push(e) })
    },
    Search() {
      //this.mapperSelected = null
      HTTP.get(URL_Name)
        .then(response => {
          this.items = response.data      
        }).catch(e => { this.errors.push(e) })
    },

    goToPosting() {
      HTTP.get(URL_Name + '/0')
          .then(response => {
            this.postingSelected = response.data
            this.postingSelected.startDate = this.setItemDate(response.data.startDate)
            this.postingSelected.endDate = this.setItemDate(response.data.endDate)
            this.postingSelectedIndex = -1
            this.drawerPosting = !this.drawerPosting
            console.log('parent = ' + this.postingSelectedIndex)
            
          }).catch(e => { this.errors.push(e) })
    },

    setItemDate(date) {
      return date.split('T')[0]
   },
    editPosting(item) {
      this.postingSelectedIndex = this.items.indexOf(item)
      this.postingSelected = Object.assign({}, item)
      this.postingSelected.startDate = this.setItemDate(item.startDate)
      this.postingSelected.endDate = this.setItemDate(item.endDate)
      this.postingSelected.entityState = 4
      this.drawerPosting = !this.drawerPosting
    },

    closePosting(value) {
      this.initialize()
      this.drawerPosting = !value.isClosed
     },
    
    getDetails(item) {
      if (item && item.id > 0) {
        this.isPosted = item.post
        HTTP.get(URL_Detail + '/' + item.id)
            .then(response => {
              this.itemDetails = response.data
              this.itemDetails.forEach(element => {
                element.effectiveDate = this.setItemDate(element.effectiveDate)
              });
          }).catch(e => { this.errors.push(e) })
      }
    },

    saveEditItems() {
      this.itemDetails.forEach(element => {
        element.entityState = 4
      });
      HTTP.put(URL_Detail + '/1', this.itemDetails)
          .then(response => { 
          //this.itemDetails = response.data
        })
          .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    },

    deleteItem (item) {
      // var bobyParam = []
      
      // item.EntityState = 8
      // bobyParam.push(item)      
      // HTTP.put(URL_Detail + '/1', bobyParam)
      //     .then(response => { 
      //       const index = this.itemDetails.indexOf(item)
      //       this.itemDetails.splice(index, 1)
      //   })
      //     .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    },

    close () {
      // this.dialog = false
      // this.$refs.form.reset()
      // setTimeout(() => {
      //   this.editedItem = Object.assign({}, this.defaultItem)
      //   this.editedIndex = -1
      //   this.mapperSelected = null
      //   this.valid = false;
      // }, 300)
    },

    Post() {
      var postingId = this.itemDetails[0].postingId
      var item = Object.assign({}, this.items.find(x => x.id == postingId))
      item.post = true
      item.entityState = 4

      HTTP.put(URL_Name + '/' + item.id, item )
        .then(response => {
          this.initialize()
        })
      .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
    }
  }
}
