<template>
  <v-flex xs12>
    <v-card>
        <v-card-text>
            <v-container fluid class="pa-0">
            <v-layout row wrap>
                <v-flex xs8 sm2>
                <v-btn color="primary" fab small dark v-if="adding" @click="newItem()">
                    <v-icon>add_circle_outline</v-icon>
                </v-btn>
                </v-flex>
                <v-flex xs8 sm2>
                <v-btn color="primary" fab small dark v-if="!adding" :disabled="!valid"  @click.native="save">
                    <v-icon>save</v-icon>
                </v-btn>
                </v-flex>
                <v-flex xs8 sm2>
                <v-btn fab small light v-if="!adding"  @click.native="close">
                    <v-icon color="pink">clear</v-icon>
                </v-btn>
                </v-flex>
            </v-layout>
            </v-container>
            <v-divider></v-divider>
        </v-card-text>
    </v-card>
    <v-card v-if="edit">
        <v-card-text>
        <v-form v-model="valid" ref="form" lazy-validation>
            <v-container grid-list-md>
            <v-layout wrap>
                <v-flex xs12 sm12 md12>
                    <v-text-field label="Mapping Name" prepend-icon="map" required
                    :rules="[v => !!v || 'Name is required']" v-model="editedItem.mapperName"></v-text-field>
                    <v-switch color="pink" :label="`Define as default mapping: ${editedItem.isDefault.toString()}`"    v-model="editedItem.isDefault"></v-switch>
                </v-flex>
            </v-layout>
            </v-container>
            <v-divider></v-divider>
        </v-form>
        </v-card-text>
    </v-card>
    <v-card>
        <v-card-text>
          <v-alert type="error" :value="true" v-for="error in errors"
          :key="error">
              <span>{{ error }}</span>
          </v-alert>
            <v-data-table
                    :headers="headers" :items="items"
                    hide-actions item-key="name">
                <template slot="items" slot-scope="props">
                <td>{{ props.item.mapperName }}</td>
                <td>
                    <v-checkbox
                        color="success"
                        hide-details
                        :input-value="props.item.isDefault">
                    </v-checkbox>
                </td>
                <td class="justify-center layout px-0">
                    <v-btn icon class="mx-0" @click="editItem(props.item)">
                    <v-icon color="teal">edit</v-icon>
                    </v-btn>
                    <v-btn icon class="mx-0" @click="deleteItem(props.item)">
                    <v-icon color="pink">delete</v-icon>
                    </v-btn>
                </td>
                </template>
            </v-data-table>
        </v-card-text>
    </v-card>
  </v-flex>
</template>

<script lang="js">
import { HTTP } from '../../api'
const URL_Name = 'CodeMappers'
  export default  {
    name: 'mapper',
    props: {
        title: {
                type: String,
                default: '6495ed'
            }
    },
    mounted() {

    },
    data() {
      return {
        drawer: null,
        editedIndex: -1,
        valid: false,
        adding: true,
        edit: false,
        errors:[],
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
        headers: [
                  { text: 'Mapping name', value: 'mapperName', align: 'left', sortable: false },
                  { text: 'Default', value: 'isDefault', sortable: false, width: '30px' },
                  { text: '_', value: 'name', sortable: false, align: 'center', width: '40px' }
                ],
        items: []
              }
    },  
    created () {
    this.initialize()
    },
    methods: {
    initialize() {
      HTTP.get(URL_Name)
        .then(response => {
          this.items = response.data
        }).catch(e => { this.errors.push(e) })
    },
    deleteItem (item) {
        HTTP.delete(URL_Name + '/' + item.id)
            .then(response => { this.initialize() })
            .catch(e => { this.errors.push(e) })
    },

    newItem () {
        this.edit = true
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.adding = false
    },

    editItem (item) {
      this.edit = true
      this.adding = false
      this.editedIndex = this.items.indexOf(item)
      this.editedItem = Object.assign({}, item)
    },

    close () {
      this.edit = false
      this.adding = true
      this.$refs.form.reset()
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.valid = false;
      }, 300)
    },

    save () {
      if (this.$refs.form.validate()){
        // this.editedItem.accountTypeId = this.accountTypeSelected.id
        var bodyParm = {
            id: this.editedItem.id,
            mapperName: this.editedItem.mapperName,
            isDefault: this.editedItem.isDefault? 1: 0,
            detailItems: []
        }

        if (this.editedIndex > -1) {
          HTTP.put(URL_Name + '/' + this.editedItem.id, this.editedItem )
              .then(response => { this.initialize() })
              .catch(e => { e? this.errors.push(e):this.errors.push('Unknown error') })
        } else {
            if (bodyParm) {
                HTTP.post(URL_Name, bodyParm)
                    .then(response => { this.initialize() })
                    .catch(e => { 
                        e? this.errors.push(e):this.errors.push('Unknown error')
                        console.log(bodyParm)
                        
                        })
            }else
                console.log('Parameter not set properly')
                
        }
        this.close()
      }
    }

    },
    computed: {

    }
}
</script>

<style scoped lang="css">
.home {
}
</style>
