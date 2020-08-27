<template>
  <q-page padding>
    <!-- content -->
    <q-toolbar color="indigo accent-1" >
      <q-btn
        flat
        round
        dense
        @click="layoutModal = true"
        icon="add"
        color="pink"
      />
    </q-toolbar>

    <q-modal v-model="layoutModal" :content-css="{minWidth: '40vw', minHeight: '40vh'}" no-backdrop-dismiss>
      <q-modal-layout>
        <q-toolbar slot="header" color="indigo accent-1">
          <q-toolbar-title>
            {{formTitle}}
          </q-toolbar-title>
          <q-btn flat round dense @click="save" :disable="$v.editedItem.$invalid"  icon="check_circle"/>
          <q-btn flat round dense @click="reset" class="q-mr-md" icon="undo"/>
          <q-btn flat round dense @click="close" icon="close"/>
        </q-toolbar>

        <div class="layout-padding">
          <q-field
            icon="comment"
            icon-color="primary"
            class="q-mt-md" 
          >
            <q-input v-model="editedItem.accountName" @blur="$v.editedItem.accountName.$touch" :error="$v.editedItem.accountName.$error" float-label="Account name" />
          </q-field>
          <q-field
            icon="public"
            icon-color="primary"
            class="q-mt-md"
          >
            <q-select
              float-label="Pick an Account type"
              v-model="editedItem.accountTypeId"
              :options="accountTypes"
            />
          </q-field>
        </div>
      </q-modal-layout>
    </q-modal>

    <q-table
      :title="title"
      :data="items"
      :columns="headers"
      row-key="id"
      :pagination.sync="serverPagination">
      <q-tr slot="body" slot-scope="props" :props="props">
        <q-td key="accountName" :props="props">{{ props.row.accountName }}</q-td>
        <q-td key="accountTypeName" :props="props">{{ props.row.accountTypeName }}</q-td>
        <q-td key="id" :props="props" class="q-mr-xs">
          <q-btn  size="sm" round dense color="indigo" icon="edit" @click="editItem(props.row)" class="q-mr-xs" />
          <q-btn  size="sm" round dense color="pink" icon="delete" @click="deleteItem(props.row)" class="q-mr-xs" />
        </q-td>
      </q-tr>
    </q-table>

  </q-page>
</template>

<script>
import { required } from 'vuelidate/lib/validators'
const URL_Name = 'Accounts'
export default {
  name: 'accounts',
  components: {}, 
  props: {},
  data :() => ({
    layoutModal: false,
    title:'Accounts',
    headers: [

      { label: 'Account', name: 'accountName', field: 'accountName', align: 'left' },
      { label: 'Account type', name: 'accountTypeName', field: 'accountTypeName', align: 'left' },
      { label: '', name: 'id', align: 'right' },
    ],
    serverPagination: {
        page: 1,
        rowsNumber: 10
    },
    items: [],
    selected: [
        { id: 1 }
      ],
    validForm: false,
    dirtyForm: false,
    editedIndex: -1,
    editedItem: {
      id: 0,
      accountName: '',
      accountTypeId: 0,
      accountTypeName: ''
    },
    defaultItem: {
      code: 0,
      accountName: '',
      accountTypeId: 0,
      accountTypeName: ''
    },
    accountTypes: [],
  }),
  
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New Account' : 'Edit ' + this.editedItem.accountName
    }
  },
  validations: {
    editedItem: {
      accountName: { required  },
      accountTypeId: { required },
    }
  },
  watch: { },
  created () {
    this.initialize()
    this.loadAccountTypes()
  },
  mounted () {

  },
  methods: {
    initialize () {
      
      this.$axios.get(URL_Name)
      .then((response) => { this.items = response.data })
      .catch(() => {
        console.log('CANNOT ACCESS Server ')
        this.$q.notify({
          color: 'negative',
          position: 'top',
          message: 'Loading failed',
          icon: 'report_problem'
        })
      })
    },
    loadAccountTypes(){       
      this.$axios.get('AccountTypes')
            .then((response) => { 
              response.data.forEach(element => {
                this.accountTypes.push({
                  label: element.accountTypeName,
                  value: element.id
                })
              })
              
             })
            .catch(() => {
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'})
              })
    },
    editItem (item) {
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.layoutModal = true
    },

    deleteItem (item) {
      const index = this.items.indexOf(item)
      this.$q.dialog({
        title: 'Confirm',
        message: 'Are you sure you want to delete this item?',
        preventClose: true,
        ok: 'Delete',
        cancel: 'Cancel'
      }).then(() => {
          this.$axios.delete(URL_Name + '/' + item.id)
            .then((response) => { 
              this.initialize()
              this.$q.notify({message:'Deleted successfully...', color: 'green'})
             })
            .catch(() => {
              console.log('CANNOT ACCESS Server ')
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'
              })
            })}).catch(() => {})
    },
    reset(){
      if (this.$v.editedItem.$invalid) {
        this.$v.editedItem.$reset()
      }
    },
    close () {
      this.layoutModal = false
      this.editedItem = Object.assign({}, this.defaultItem)
      this.editedIndex = -1
    },

    save () {
      if (!this.$v.editedItem.$invalid) {
      if (this.editedIndex > -1) {
         this.$axios.put(URL_Name + '/' + this.editItem.id, this.editedItem)
            .then((response) => { 
              this.initialize()
              this.$q.notify({message:'Updated successfully...', color: 'green'})
             })
            .catch(() => {
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'})
              })
      } else {
          this.$axios.post(URL_Name, this.editedItem )
            .then((response) => { 
              this.initialize()
              this.$q.notify({message:'Updated successfully...', color: 'green'})
             })
            .catch(() => {
              this.$q.notify({
                color: 'negative',
                position: 'top',
                message: 'Loading failed',
                icon: 'report_problem'})
              })
      }
        this.close()
      }
    }
  }
}
</script>

<style>
</style>
