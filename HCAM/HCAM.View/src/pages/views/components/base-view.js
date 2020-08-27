export default {
  name: 'base-view',
  components: {}, 
  props: {},
  data :() => ({
      upTitle:'Sub title',
      title: 'Base Title',
      items: []
  }),
  
  computed: { },
  validations: { },
  watch: { },
  created () {
    this.initialize()
  },
  mounted () {

  },
  methods: {
    get (modelName) {
        this.$axios.get(modelName)
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

    update (modelName, item) {
        this.$axios.put(modelName + '/' + item.id, item)
            .then((response) => { 
                if (response.data.success) {
                    this.initialize(modelName)
                    this.$q.notify({message:'Updated successfully...', color: 'green'})
                } else {
                    this.$q.notify({
                      color: 'negative',
                      position: 'top',
                      message: response.data.error,
                      icon: 'report_problem'
                    })
                  }
         })
        .catch(() => {
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'})
          })
    },

    delete (modelName, item) {
      const index = this.items.indexOf(item)
      this.$q.dialog({
        title: 'Confirm',
        message: 'Are you sure you want to delete this item?',
        preventClose: true,
        ok: 'Delete',
        cancel: 'Cancel'
      }).then(() => {
          this.$axios.delete(modelName + '/' + item.id)
              .then((response) => { 
                
                if (response.data.success) {
                    this.initialize(modelName)
                    this.$q.notify({message:'Deleted successfully...', color: 'green'})
                } else {
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
            })}).catch(() => {})
    },

    add (modelName, item) {
        this.$axios.post(modelName, item )
        .then((response) => { 
          this.initialize()
          this.$q.notify({message:'Added successfully...', color: 'green'})
         })
        .catch(() => {
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'})
          })
      },
  }
}