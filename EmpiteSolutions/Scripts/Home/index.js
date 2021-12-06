import Vue from 'vue';
import HomeComponent from './home.vue';
import ContactComponent from './contact.vue';
import AboutComponent from './about.vue';

new Vue({
    el: "#app",
    components: {
        HomeComponent,
        ContactComponent,
        AboutComponent
    }
});