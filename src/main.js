import Vue from 'vue';
import App from './App.vue';
import { Amplify, Auth } from 'aws-amplify';
import aws_config from './aws-exports';
import {
  applyPolyfills,
  defineCustomElements,
} from '@aws-amplify/ui-components/loader';

Amplify.configure(aws_config);

Auth.configure(aws_config);

applyPolyfills().then(() => {
  defineCustomElements(window);
});

Vue.config.ignoredElements = [/amplify-\w*/];

new Vue({
  render: (h) => h(App),
}).$mount('#app');
