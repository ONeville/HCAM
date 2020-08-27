<template>
  <v-layout>
    <v-flex xs12>
      <v-card>
        <v-card-title primary-title>
          <div>
            <p>{{upTitle}}</p>
            <h2 class="headline mb-0">{{title}}</h2>
            <v-divider></v-divider>
          </div>
        </v-card-title>
      </v-card>
      <template>
        <div>
          <v-dialog v-model="dialog" max-width="400px">
            <v-btn color="primary" dark slot="activator">
            <v-icon>add_circle_outline</v-icon>New</v-btn>
            <v-card>
              <v-card-title  color="blue darken-3">
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>
              <v-card-text>
                <v-form v-model="valid" ref="form" lazy-validation>
                <v-container grid-list-md>
                  <v-layout wrap>
                    <v-flex xs12 sm12 md12>
                      <v-text-field label="Account Name" prepend-icon="map" required
                      :rules="[v => !!v || 'Account is required']" v-model="editedItem.accountName"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm12 md12>
                      <v-select label="Select account type" autocomplete  required :items="accountTypes"
                          v-model="accountTypeSelected" item-text="accountTypeName" item-value="id" :return-object="true" prepend-icon="map"
                          :rules="[v => !!v || 'Account Type is required']">
						          </v-select>
                    </v-flex>
                  </v-layout>
                </v-container>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn class="mb-2" color="primary" :disabled="!valid"  @click.native="save">Save</v-btn>
                <v-btn class="mb-2" color="primary"  @click.native="close">Cancel</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-btn color="primary" dark class="mb-2">Save</v-btn>
          <v-data-table :headers="headers" :items="items" hide-actions :loading="false" class="elevation-1">
            <v-progress-linear slot="progress" color="blue" indeterminate></v-progress-linear>
            <template slot="items" slot-scope="props">
              <td >{{ props.item.accountName }}</td>
              <td >{{ props.item.accountTypeName }}</td>
              <td class="justify-center layout px-0">
                <v-btn icon class="mx-0" @click="editItem(props.item)">
                  <v-icon color="teal">edit</v-icon>
                </v-btn>
                <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                  <v-icon color="pink">delete</v-icon>
                </v-btn>
              </td>
            </template>
            <template slot="no-data">
              <span>Not data found!</span>
            </template>
          </v-data-table>
        </div>
      </template>
    </v-flex>
  </v-layout>
</template>

<script>
//align: 'left' | 'center' | 'right'
import { HTTP } from '../../api';
const URL_Name = 'Accounts'
export default {
  name: 'account',
  components: {}, 
  props: {},
  data :() => ({
    dialog: false,
    valid: false,
    radioGroup: 1,
    upTitle:'account',
    title:'Accounts',
    headers: [
      { text: 'Account', align: 'left', value: 'accountName' },
      { text: 'Account type', value: 'accountTypeName', align: 'center' },
      { text: 'Actions', value: 'name', sortable: false, align: 'center' }
    ],
    items: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      accountName: '',
      AccountName: '',
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
    accountTypeSelected: null
  }),
  
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New account' : 'Edit account'
    }
  },
  watch: {
    dialog (val) {
      val || this.close()
    }
  },
  created () {
    this.initialize()
    this.loadAccountTypes()
  },
  mounted () {

  },
  methods: {
    initialize () {
      this.accountTypeSelected = null
      HTTP.get(URL_Name)
          .then(response => { this.items = response.data })
          .catch(e => { this.errors.push(e) })
    },

    editItem (item) {
      this.$refs.form.reset()
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.accountTypeSelected = { id: item.accountTypeId, accountTypeName: item.accountTypeName}
      this.dialog = true
    },

    loadAccountTypes(){
      HTTP.get('AccountTypes')
          .then(response => { this.accountTypes = response.data })
          .catch(e => { this.errors.push(e) })
    },

    deleteItem (item) {
        HTTP.delete(URL_Name + '/' + item.id)
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
    },

    close () {
      this.dialog = false
      this.$refs.form.reset()
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.accountTypeSelected = null
        this.valid = false;
      }, 300)
    },

    save () {
      if (this.$refs.form.validate()){
        this.editedItem.accountTypeId = this.accountTypeSelected.id
        this.editedItem.accountTypeName = this.accountTypeSelected.accountTypeName
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
</script>

