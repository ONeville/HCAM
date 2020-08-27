<template>
    <div>
    <button 
      class="switch notes" 
      @click="switchComponent('notes-list')"
      :disabled="currentComp === 'notes-list'"
    >notes</button>
    <button 
      class="switch messages"
      @click="switchComponent('messages-list')"
      :disabled="currentComp === 'messages-list'"
    >messages</button>
    </div>
</template>

<script>
//import { bus } from '../index.js';

export default {
    props: {
        currentComp: {
            type: String,
            required: true
        }
    },

    methods: {
        switchComponent(comp) {
          console.log('Recieved: ' + comp)
            this.$emit('switchComp', comp)
        }
    }
}
</script>

<style scoped>
   
button.switch {
  background: none;
  border: 1px solid black;
  margin: 0;
  cursor: pointer;
}

button.switch[disabled='disabled'] {
  color: lightgray;
  border-color: lightgray;
}

button.switch:not([disabled='disabled']):hover {
  background-color: black;
  border-color: gray;
  color: lightgray;
}

button.messages {
  border-color: red;
  color: red;
}


button.notes {
  border-color: green;
  color: green; 
} 
</style>
