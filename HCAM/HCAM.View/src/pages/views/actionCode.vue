<template>
  <q-page padding>

    <q-toolbar color="indigo accent-1" >
      <q-btn
        flat
        round
        dense
        @click="layoutModal = true"
        icon="add"
        label="New action code"
        color="pink"
      />
    </q-toolbar>

    <q-modal v-model="layoutModal" :content-css="{minWidth: '40vw', minHeight: '60vh'}" no-backdrop-dismiss>
      <q-modal-layout>
        <q-toolbar slot="header" color="indigo accent-1">
          <q-toolbar-title>
            {{formTitle}}
          </q-toolbar-title>
          <q-btn flat round dense @click="save" :disable="$v.editedItem.description.$invalid"  icon="check_circle"/>
          <q-btn flat round dense @click="reset" class="q-mr-md" icon="undo"/>
          <q-btn flat round dense @click="close" icon="close"/>
        </q-toolbar>

        <div class="layout-padding">

          <q-field
            icon="comment"
            icon-color="primary"
          >
            <q-input v-model="editedItem.name" @blur="$v.editedItem.name.$touch" :error="$v.editedItem.name.$error" float-label="Action Code"/>
          </q-field>


          <q-field
            icon="edit"
            icon-color="primary"
            class="q-mt-md" 
          >
            <q-input v-model="editedItem.description" @blur="$v.editedItem.description.$touch" :error="$v.editedItem.description.$error" float-label="Description" />
          </q-field>
          

          <q-option-group
            class="q-mt-md"
            type="radio"
            v-model="editedItem.codeType"
            :options="[
              { label: 'Is Fee', value: 'isFee' },
              { label: 'Is Asset', value: 'isAsset', color: 'secondary' },
              { label: 'Is Liability', value: 'isLiability', color: 'amber' },
              { label: 'Is Equity', value: 'isEquity', color: 'amber' }
            ]"
          />

          <!-- <q-input
            v-model="form.email"
            @blur="$v.form.email.$touch"
            @keyup.enter="submit"
            :error="$v.form.email.$error"
          /> -->

        </div>
      </q-modal-layout>
    </q-modal>


    <!-- <p class="caption">Custom Table</p> -->
    <q-table
      title="Action Codes"
      :data="items"
      :columns="headers"
      row-key="id"
      :pagination.sync="serverPagination">
      <q-tr slot="body" slot-scope="props" :props="props">
        <q-td key="code" :props="props">{{ props.row.code }}</q-td>
        <q-td key="name" :props="props">{{ props.row.name }}</q-td>
        <q-td key="description" :props="props">{{ props.row.description }}</q-td>
        <q-td key="isFee" :props="props">
          <q-checkbox v-model="props.row.isFee" color="secondary" readonly/>
        </q-td>
        <q-td key="isAsset" :props="props">
          <q-checkbox v-model="props.row.isAsset" color="secondary" readonly/>
        </q-td>
        <q-td key="isLiability" :props="props">
          <q-checkbox v-model="props.row.isLiability" color="secondary" readonly/>
        </q-td>
        <q-td key="isEquity" :props="props">
          <q-checkbox v-model="props.row.isEquity" color="secondary" readonly/>
        </q-td>
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
const URL_Name = 'ActionCodes'
export default {
  name: 'action-code',
  components: {}, 
  props: {},
  data :() => ({
    layoutModal: false,
    radioGroup: 1,
    upTitle:'action code',
    title:'Action Codes',
    headers: [
      {
        label: 'Action Code',
        align: 'left',
        sortable: false,
        name: 'code',
        field: 'code'
      },
      { label: 'Name', field: 'name', name: 'name', align: 'left' },
      { label: 'Description', field: 'description', name: 'description', align: 'left' },
      { label: 'Is Fee', field: 'isFee', name: 'isFee', align: 'center' },
      { label: 'Is Asset', field: 'isAsset', name: 'isAsset', align: 'center' },
      { label: 'Is Liability', field: 'isLiability', name: 'isLiability', align: 'center' },
      { label: 'Is Equity', field: 'isEquity', name: 'isEquity', align: 'center' },
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
      code: 0,
      name: '',
      description: '',
      isFee: 0,
      isAsset: 0,
      isLiability: 0,
      isEquity: 0,
      codeType: 'isFee'
    },
    defaultItem: {
      code: 0,
      name: '',
      description: '',
      isFee: 0,
      isAsset: 0,
      isLiability: 0,
      isEquity: 0,
      codeType: 'isFee'
    }
  }),
  
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New Action Code' : 'Edit Item'
    }
  },
  validations: {
    editedItem: {
      name: { required  },
      description: { required },
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
      this.$axios.get(URL_Name, { crossdomain: true })
      .then((response) => { 
          if (response.data.success) {
            this.items = response.data.value
          }else{
            this.$q.notify({
              color: 'negative',
              position: 'top',
              message: response.data.error,
              icon: 'report_problem'
            })
          }
        })
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
      //this.$v.editedItem.$reset()
      this.editedItem = Object.assign({}, this.defaultItem)
      this.editedIndex = -1
    },

    save () {
      this.setActionCodeType(this.editedItem)
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

    setActionCodeType(item){
      switch (item.codeType) {
        case 'isFee':
          item.isFee = 1
          break;
        case 'isAsset':
          item.isAsset = 1
          break;
        case 'isLiability':
          item.isLiability = 1
          break;
        case 'isEquity':
          item.isEquity = 1
          break;
        default:
          break;
      }
    },
    
    getActionCodeType(item){
      if (item.isFee == 1) {
        item.codeType = 'isFee'
      }
      if (item.isAsset == 1) {
        item.codeType = 'isAsset'
      }
      if (item.isLiability == 1) {
        item.codeType = 'isLiability'
      }
      if (item.isEquity == 1) {
        item.codeType = 'isEquity'
      }
    }
  }
}
</script>

<style>
</style>
