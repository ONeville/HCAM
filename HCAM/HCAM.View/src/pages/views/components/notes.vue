<template>
  <div>
    <span class="label">notes list</span>  
    
    <div class="container">
      <div
        :key="note.id"
        v-for="note in notes"
      >- {{ note.text }}</div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      notes: [{
        id: 1,
        text: 'Marco',
      }, {
        id: 2,
        text: 'Polo'
      }]
    };
  }
}
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  width: 400px;
  height: 200px;
  margin: 5px;
  padding: 10px;
  border: 1px solid green;
}

.label {
  font-size: 10px;
  color: green;
}
</style>
