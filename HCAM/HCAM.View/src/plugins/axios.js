import axios from 'axios'

const URL_BASE = 'http://localhost:55552/api/'
// export default ({ Vue }) => {
//   axios.defaults.baseURL = '/55552/api'
//   Vue.prototype.$axios = axios
// }
export default ({ Vue }) => {
  Vue.prototype.$axios = axios.create({
    baseURL: URL_BASE,
    // headers: {
    //   // Authorization: 'Bearer {token}',
    //   "Access-Control-Allow-Origin": "http://localhost:55552",
    //   "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept",
    //   // 'Access-Control-Allow-Origin': '*',
    //   // Accept: 'application/json; charset=utf-8'
    // }
  })
}
