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
                      <v-text-field label="Action Code" v-model="editedItem.name"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md8>
                      <v-text-field label="Description" v-model="editedItem.description"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md8>
                      <v-radio-group v-model="editedItem.codeType" :mandatory="false">
                        <v-radio label="Is Fee" value="isFee"></v-radio>
                        <v-radio label="Is Asset" value="isAsset"></v-radio>
                        <v-radio label="Is Liability" value="isLiability"></v-radio>
                        <v-radio label="Is Equity" value="isEquity"></v-radio>
                      </v-radio-group>
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
              <td >{{ props.item.code }}</td>
              <td>{{ props.item.name }}</td>
              <td>{{ props.item.description }}</td>
              <td>{{ props.item.isFee }}</td>
              <td>{{ props.item.isAsset }}</td>
              <td>{{ props.item.isLiability }}</td>
              <td>{{ props.item.isEquity }}</td>
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
const URL_Name = 'ActionCodes'
export default {
  name: 'action-code',
  components: {}, 
  props: {},
  data :() => ({
    dialog: false,
    radioGroup: 1,
    upTitle:'action code',
    title:'Action Codes',
    headers: [
      {
        text: 'Action Code',
        align: 'left',
        sortable: false,
        value: 'code'
      },
      { text: 'Name', value: 'name', align: 'center' },
      { text: 'Description', value: 'description', align: 'center' },
      { text: 'Is fee', value: 'isFee', align: 'center' },
      { text: 'Is asset', value: 'isAsset', align: 'center' },
      { text: 'Is liability', value: 'isLiability', align: 'center' },
      { text: 'Is equity', value: 'isEquity', align: 'center' },
      { text: 'Actions', value: 'name', sortable: false, align: 'center' }
    ],
    items: [],
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
      return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
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
      this.getActionCodeType(item)
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
      //const index = this.items.indexOf(item)
        HTTP.delete(URL_Name + '/' + item.id)
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
      //confirm('Are you sure you want to delete this item?') && this.items.splice(index, 1)
    },

    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },

    save () {
      this.setActionCodeType(this.editedItem)
      if (this.editedIndex > -1) {
        //Object.assign(this.items[this.editedIndex], this.editedItem)
        HTTP.put(URL_Name + '/' + this.editItem.id, this.editedItem )
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
      } else {
        //this.items.push(this.editedItem)
        HTTP.post(URL_Name, this.editedItem )
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
      }
      this.close()
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

