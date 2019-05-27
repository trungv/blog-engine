import Vue from "vue";
import Vuesax from 'vuesax';
import "vuesax/dist/vuesax.css"; // import css style
import "material-icons/iconfont/material-icons.css";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css"
import "./registerServiceWorker";
import VeeValidate from "vee-validate";

Vue.use(Vuesax);
Vue.use(VeeValidate);
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
