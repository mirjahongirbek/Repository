import Vue from 'vue';
import App from './App.vue';
import { ServerTable, ClientTable, Event } from 'vue-tables-2';

Vue.config.productionTip = true;
Vue.use(ServerTable);
Vue.use(ClientTable);
new Vue({
    render: h => h(App)
}).$mount('#app');
