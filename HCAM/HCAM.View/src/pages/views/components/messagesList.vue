s<template>
  <div>
    <span class="label">message list</span>  
    
    <div class="container">
      <div
        :key="msg.id"
        v-for="msg in messages"
      >- {{ msg.text }}</div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      messages: [{
        id: 1,
        text: 'Kung Fu'
      },
      {
        id: 2,
        text: 'Panda'
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
  border: 1px solid red;
}

.label {
  font-size: 10px;
  color: red;
}
</style>
