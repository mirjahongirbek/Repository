import Vue from "vue"
import Vuex from "vuex"
import axios from "axios"
Vue.use(Vuex);
let someUrl=axios.create({
    baseURL:"localhost:1234"
})
var store = new Vuex.Store({
    state: {
        http: someUrl
    }
});

export default store;