<template>
  <q-dialog
    v-model="customDialogModel"
    stack-buttons
    prevent-close
    @ok="onOk"
    @cancel="onCancel"
    @show="onShow"
    @hide="onHide"
  >
    <!-- This or use "title" prop on <q-dialog> -->
    <span slot="title">Favorite Superhero</span>

    <!-- This or use "message" prop on <q-dialog> -->
    <span slot="message">What is your superhero of choice?</span>

    <div slot="body">
      <q-field
        icon="account_circle"
        helper="We need your name so we can send you to the movies."
        label="Your name"
        :label-width="3"
      >
        <q-input v-model="name" />
      </q-field>
    </div>

    <template slot="buttons" slot-scope="props">
      <q-btn color="primary" label="Choose Superman" @click="choose(props.ok, 'Superman')" />
      <q-btn color="black" label="Choose Batman" @click="choose(props.ok, 'Batman')" />
      <q-btn color="negative" label="Choose Spiderman" @click="choose(props.ok, 'Spiderman')" />
      <q-btn flat label="No thanks" @click="props.cancel" />
    </template>
  </q-dialog>
</template>

<script>
export default {
  data () {
    return {
      // model for Dialog example
      customDialogModel: false,

      name: ''
    }
  },
  methods: {
    // when props.ok() gets called
    onOk (data) { },

    // when props.cancel() gets called
    onCancel () { },

    // when we show it to the user
    onShow () { },

    // when it gets hidden
    onHide () { },

    // custom handler
    async choose (okFn, hero) {
      if (this.name.length === 0) {
        this.$q.dialog({
          title: 'Please specify your name!',
          message: `Can't buy tickets without knowing your name.`
        })
      }
      else {
        await okFn()
        this.$q.notify(`Ok ${this.name}, going with ${hero}`)
      }
    }
  }
}
</script>