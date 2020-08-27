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
          <v-dialog v-model="dialog" max-width="500px">
            <v-btn color="primary" dark slot="activator" class="mb-2">New Item</v-btn>
            <v-card>
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>
              <v-card-text>
                <v-container grid-list-md>
                  <v-layout wrap>
                    <v-flex xs12 sm6 md8>
                      <v-text-field label="Account Type" v-model="editedItem.accountTypeName"></v-text-field>
                    </v-flex>
                  </v-layout>
                </v-container>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
                <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-data-table :headers="headers" :items="items" hide-actions :loading="false" class="elevation-1">
            <v-progress-linear slot="progress" color="blue" indeterminate></v-progress-linear>
            <template slot="items" slot-scope="props">
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
const URL_Name = 'AccountTypes'
export default {
  name: 'account-type',
  components: {}, 
  props: {},
  data :() => ({
    dialog: false,
    radioGroup: 1,
    upTitle:'account type',
    title:'Account Types',
    headers: [
      {
        text: 'Name',
        align: 'left',
        value: 'accountTypeName'
      },
      { text: 'Actions', value: 'name', sortable: false, align: 'center' }
    ],
    items: [],
    editedIndex: -1,
    editedItem: {
      id: 0,
      accountTypeName: '',
    },
    defaultItem: {
      code: 0,
      accountTypeName: '',
    }
  }),
  
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New account types' : 'Edit account types'
    }
  },
  watch: {
    dialog (val) {
      val || this.close()
    }
  },
  created () {
    this.initialize()
  },
  mounted () {

  },
  methods: {
    initialize () {
      HTTP.get(URL_Name)
          .then(response => { this.items = response.data })
          .catch(e => { this.errors.push(e) })
    },

    editItem (item) {
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
        HTTP.delete(URL_Name + '/' + item.id)
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
    },

    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },

    save () {
      if (this.editedIndex > -1) {
        HTTP.put(URL_Name + '/' + this.editItem.id, this.editedItem )
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
      } else {
        HTTP.post(URL_Name, this.editedItem )
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
      }
      this.close()
    }
  }
}
</script>

