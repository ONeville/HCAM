<template>
  <q-page padding>
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

    <q-modal v-model="layoutModal" :content-css="{minWidth: '40vw', minHeight: '20vh'}" no-backdrop-dismiss>
      <q-modal-layout>
        <q-toolbar slot="header" color="indigo accent-1">
          <q-toolbar-title>
            {{formTitle}}
          </q-toolbar-title>
          <q-btn flat round dense @click="save" :disable="$v.editedItem.accountTypeName.$invalid"  icon="check_circle"/>
          <q-btn flat round dense @click="reset" class="q-mr-md" icon="undo"/>
          <q-btn flat round dense @click="close" icon="close"/>
        </q-toolbar>

        <div class="layout-padding">
          <q-field
            icon="comment"
            icon-color="primary"
            class="q-mt-md" 
          >
            <q-input v-model="editedItem.accountTypeName" @blur="$v.editedItem.accountTypeName.$touch" :error="$v.editedItem.accountTypeName.$error" float-label="Account type name" />
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
const URL_Name = 'AccountTypes'
export default {
  name: 'action-code',
  components: {}, 
  props: {},
  data :() => ({
    layoutModal: false,
    radioGroup: 1,
    upTitle:'account type',
    title:'Account Types',
    headers: [
      { label: 'Account type name', field: 'accountTypeName', name: 'accountTypeName', align: 'left' },
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
    editedIndex: -1,
    editedItem: {
      id: 0,
      accountTypeName: '',
    },
    defaultItem: {
      id: 0,
      accountTypeName: '',
    }
  }),
  
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New Account Type' : 'Edit ' + this.editedItem.accountTypeName
    }
  },
  validations: {
    editedItem: {
      accountTypeName: { required  }
    }
  },
  watch: { },
  created () {
    this.initialize()
  },
  mounted () {

  },
  methods: {
    initialize () {
      this.$axios.get(URL_Name)
      .then((response) => { this.items = response.data })
      .catch(() => {
        this.$q.notify({
          color: 'negative',
          position: 'top',
          message: 'Loading failed',
          icon: 'report_problem'
        })
      })
    },

    editItem (item) {
      this.getActionCodeType(item)
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
    },
  }
}
</script>

<style>
</style>
