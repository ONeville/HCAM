<template>
  <q-page padding>
    <!-- content -->
    <q-toolbar color="indigo accent-1" >
      <q-btn flat round dense @click="swapComponent" icon="settings" color="pink" />
      <q-btn flat round dense @click="layoutModal = true" icon="save" color="pink" />
    </q-toolbar>

    <q-field
      icon="class"
      icon-color="primary"
      class="q-mt-md">
      <q-select
        float-label="Pick a Mapping"
        v-model="mappingId"
        :options="items"
        @input="changedMapping"
      />
    </q-field>
    
    <q-card>
      


             <button 
                class="switch notes" 
                @click="switchComponent('notes-list')">notes</button>
              <button 
                class="switch messages"
                @click="switchComponent('messages-list')">messages</button>

        <div>
          <component :is="currentComp"></component>
        </div>
    </q-card>
    
    <q-table
      :title="title"
      :data="itemDetails"
      :columns="headers"
      row-key="actionCodeId"
      :pagination.sync="serverPagination">
      <q-tr slot="body" slot-scope="props" :props="props">

        <q-td key="actionCode" :props="props">{{ props.row.actionCode }}</q-td>
        <q-td key="actionCodeName" :props="props">{{ props.row.actionCodeName }}</q-td>
        <q-td key="creditAccountName" :props="props">{{ props.row.creditAccountName }}</q-td>
        <q-td key="debitAccountName" :props="props">{{ props.row.debitAccountName }}</q-td>
        <q-td key="accountingCode" :props="props">{{ props.row.accountingCode }}</q-td>
        <q-td key="actionCodeId" :props="props" class="q-mr-xs">
          <q-btn  size="sm" round dense color="pink" icon="delete" @click="deleteItem(props.row)" class="q-mr-xs" />
        </q-td>
      </q-tr>
    </q-table>
  </q-page>
</template>

<script>
import { required } from 'vuelidate/lib/validators'
const URL_Name = 'CodeMappers'
const URL_Detail = 'CodeMapperDetails'

import MapperTag from './mapper.vue'

import Toolbar from './components/toolbar.vue';
import MessagesList from './components/messagesList.vue';
import Notes from './components/notes.vue';

export default {
  name: 'code-mapping',
  components: { 
    'editMapping' : {
      template: '<h1>Bar component</h1>'
    },
    'toolbar': Toolbar,
    'messages-list': MessagesList,
    'notes-list': Notes
   },
  props: [],
  data () {
    return {
      dialog: false,
      valid: false,
      expanded:false,
      currentComponent: null,
      currentComp: 'notes-list',
      loader: null,
      drawerMapper: null,
      loading3: false,
      upTitle:'Mapping codes',
      subTitle:'Mapping Setter',
      title:'Accounting Mappings',
      headers: [
        { label: 'Code', name: 'actionCode', field: 'actionCode', align: 'left' },
        { label: 'Action Code', name: 'actionCodeName', field: 'actionCodeName', align: 'left' },
        { label: 'Credit Account', name: 'creditAccountName', field: 'creditAccountName', align: 'left' },
        { label: 'Debit Account', name: 'debitAccountName', field: 'debitAccountName', align: 'left' },
        { label: 'Accounting Code', name: 'accountingCode', field: 'accountingCode', align: 'left' },
        { label: '', name: 'actionCodeId', align: 'right' }
      ],
      serverPagination: {
          page: 1,
          rowsNumber: 10
      },
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
      mappingId:0,
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

      this.$axios.get(URL_Name + '/null')
      .then((response) => {
          this.mappingId = response.data.id
          this.itemDetails = response.data.detailItems;
          this.getAccounts()   })
      .catch(() => {
        console.log('CANNOT ACCESS Server ')
        this.$q.notify({
          color: 'negative',
          position: 'top',
          message: 'Loading failed',
          icon: 'report_problem'
        })})
    },

    getMappings() {
      this.$axios.get(URL_Name)
      .then((response) => {
              response.data.forEach(element => {
                this.items.push({
                  label: element.mapperName,
                  value: element.id
                })
              })
      })
      .catch(() => {
        console.log('CANNOT ACCESS Server ')
        this.$q.notify({
          color: 'negative',
          position: 'top',
          message: 'Loading failed',
          icon: 'report_problem'
        })})

    },

    generateMapping() {
      if (this.mappingId > 0) {
        this.$axios.put(URL_Detail + '/' + this.mappingId)
        .then((response) => { this.itemDetails = response.data
            this.loader = null })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})
      }
    },

    getAccounts() {
      this.$axios.get('Accounts')
        .then((response) => {
          response.data.forEach(element => {
                this.accounts.push({
                  label: element.accountName,
                  value: element.id
                })
              })
        })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})

    },

    editItem (item) {
      // this.$refs.form.reset()
      // this.editedIndex = this.items.indexOf(item)
      // this.editedItem = Object.assign({}, item)
      // this.dialog = true
    },

    changedMapping(value) {
      //console.log('Selected: ' + value)
      this.$axios.get(URL_Name + '/'+ value)
        .then((response) => {
          this.mappingId = response.data.id
          this.itemDetails = response.data.detailItems;
          this.getAccounts()
        })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})

    },

    saveEditItems() {
      this.$axios.put(URL_Detail + '/1', this.itemDetails)
        .then((response) => { })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})
    },

    loadAccountTypes(){
      this.$axios.get('AccountTypes')
        .then((response) => { this.accountTypes = response.data })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})

    },

    deleteItem (item) {
      var bobyParam = []
      item.EntityState = 8
      bobyParam.push(item)
      this.$axios.put(URL_Detail + '/1', bobyParam)
        .then((response) => {
            const index = this.itemDetails.indexOf(item)
            this.itemDetails.splice(index, 1)
        })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})
    },

    close () {
      this.dialog = false
      this.$refs.form.reset()
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.valid = false;
      }, 300)
    },

    setDefault () {
      var bobyParam = {
        id: this.mappingId,
        mapperName: this.items.filter(x=>x.id == this.mappingId).mapperName,
        isDefault: true,
        detailItems: []
      }
      this.$axios.put(URL_Name + '/' + bobyParam.id, bobyParam )
        .then((response) => { this.initialize() })
        .catch(() => {
          console.log('CANNOT ACCESS Server ')
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })})

    },

    save () {
      if (this.$refs.form.validate()){
        if (this.editedIndex > -1) {

            this.$axios.put(URL_Name + '/' + this.editedItem.id, this.editedItem )
            .then((response) => { this.initialize() })
            .catch(() => {
              console.log('CANNOT ACCESS Server ')
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'
              })})
        } else {

          this.$axios.post(URL_Name, this.editedItem )
            .then((response) => { this.initialize() })
            .catch(() => {
              console.log('CANNOT ACCESS Server ')
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'
              })})
        }
        this.close()

      }
    },
    swapComponent(){
      // this.currentComponent = 'editMapping'
      // this.currentComp = 'messages-list'
    },
    created() {
      this.$v.$on('switchComp', comp => {
        console.log('Send: ' + comp)
           this.currentComp = comp
      })
  },
  switchComponent(comp) {
  console.log('Recieved: ' + comp)
    this.currentComp = comp
}

  }
}
</script>

<style>
</style>
