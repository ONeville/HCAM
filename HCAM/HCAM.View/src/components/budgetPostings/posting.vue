<template>
  <v-flex xs12>
    <v-card>
        <v-card-text>
            <v-container fluid class="pa-0">
            <v-layout row wrap>
                <v-flex xs8 sm2>
                    <v-btn color="primary" fab small dark :disabled="!valid"  @click.native="Save">
                        <v-icon>save</v-icon>
                    </v-btn>
                </v-flex>
                <v-flex xs8 sm2  v-if="curentIndex > -1">
                    <v-btn color="pink" fab small dark @click.native="Delete">
                        <v-icon>delete</v-icon>
                    </v-btn>
                </v-flex>
            </v-layout>
            </v-container>
            <v-divider></v-divider>
        </v-card-text>
    </v-card>
    <v-card>
      <v-card-text>
        <v-form v-model="valid" ref="form" lazy-validation>
            <v-container grid-list-md>
            <v-layout wrap>
                <v-flex xs12 sm12 md12>
                    <v-text-field label="Mapping" prepend-icon="swap_horiz" readonly
                    v-model="currentItem.codeMapping" class="input-group--focused"></v-text-field>
                    <v-text-field label="Description" prepend-icon="comment" required
                    :rules="[v => !!v || 'Description is required']" v-model="currentItem.description"></v-text-field>      
                </v-flex>
                <v-flex xs11 sm5>
                    <v-menu ref="menuStart" lazy :close-on-content-click="false" v-model="menuStart" transition="scale-transition" offset-y full-width :nudge-right="40" min-width="290px" :return-value.sync="currentItem.startDate">
                        <v-text-field slot="activator" label="Start Date" v-model="currentItem.startDate" prepend-icon="event" readonly class="input-group--focused"></v-text-field>
                        <!-- mask="####-##-##" -->
                        <v-date-picker v-model="currentItem.startDate" no-title scrollable>
                            <v-btn flat color="primary" @click="menuStart = false">Cancel</v-btn>
                            <v-btn flat color="primary" @click="$refs.menuStart.save(currentItem.startDate)">OK</v-btn>
                        </v-date-picker>
                    </v-menu>
                </v-flex>
                <v-flex xs11 sm5>
                    <v-menu
                        ref="menuEnd" lazy :close-on-content-click="false"
                        v-model="menuEnd" transition="scale-transition" offset-y full-width :nudge-right="40"
                        min-width="290px">
                        <v-text-field
                        slot="activator" label="Birthday date" v-model="currentItem.endDate" prepend-icon="event" readonly></v-text-field>
                        <v-date-picker
                        ref="picker" v-model="currentItem.endDate" @change="saveEndDate" :title-date-format="dt=>new Date(dt).toISOString().substr(0, 10)" min="1950-01-01" :max="new Date().toISOString().substr(0, 10)"></v-date-picker>
                    </v-menu>
                    <!-- <v-menu ref="menuEnd" lazy :close-on-content-click="false" v-model="menuEnd" transition="scale-transition" offset-y full-width :nudge-right="40" min-width="290px" :return-value.sync="currentItem.endDate">
                        <v-text-field slot="activator" label="End Date" v-model="currentItem.endDate" prepend-icon="event" readonly class="input-group--focused"></v-text-field>
                        <v-date-picker v-model="currentItem.endDate" no-title scrollable>
                            <v-btn flat color="primary" @click="menuEnd = false">Cancel</v-btn>
                            <v-btn flat color="primary" @click="$refs.menuEnd.save(currentItem.endDate)">OK</v-btn>
                        </v-date-picker>
                    </v-menu> -->
                </v-flex>
            </v-layout>
            </v-container>
        </v-form>
      </v-card-text>
    </v-card>
  </v-flex>
</template>

<script lang="js">
import { HTTP } from '../../api'
const URL_Name = 'Postings'
  export default  {
    name: 'postings',
    props: {
        curentIndex: {
            type: Number,
            default: -1
        },
        currentItem: {
            type: Object,
            default: {
                id: 0,
                codeMappingId: 0,
                codeMapping: 'Default Mapping',
                startDate: '2015-01-01',
                startDateText: '2015-01-01',
                endDate: '2015-01-10',
                endDateText: '2015-01-01',
                description: 'Test',
                post: false,
                entityState: 0,
            }
        }
    },
    mounted() {
            this.valid = true
    },
    data() {
      return {
        menuStart: false,
        menuEnd: false,
        valid: true,
        edit:false,
        isClosed:false,
        errors:[],
      }
    },  
    created () {
     this.initialize()
    },
    watch: {
        menuEnd (val) {
            val && this.$nextTick(() => (this.$refs.picker.activePicker = 'YEAR'))
        }
    },
    methods: {
        initialize() {
            this.valid = true          
        },
        saveEndDate (date) {
            this.$refs.menuEnd.save(date)
        },

        Save(){
            if (this.$refs.form.validate()) {
                HTTP.post(URL_Name, this.currentItem)
                    .then(response => {
                            this.Close()
                    }).catch(e => { this.errors.push(e) })
            }
        },

        Delete(){
            if (this.currentItem && this.currentItem.id > 0) {
                HTTP.delete(URL_Name, this.currentItem.id)
                    .then(response => {
                        this.Close()
                    }).catch(e => { this.errors.push(e) })
            }
        },

        Close(){
            this.$refs.form.reset()
            this.valid = true
            this.edit = false
            this.isClosed = true
            this.$emit('isClosed', { isClosed: this.isClosed })
        },



    },
    computed: {
    }
}
</script>

