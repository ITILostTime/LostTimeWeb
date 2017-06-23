import 'babel-polyfill'
import $ from 'jquery'
import 'bootstrap/dist/js/bootstrap'
import Vue from 'vue'
import store from './vuex/store'
import VueRouter from 'vue-router'
import moment from 'moment'

import App from './components/App.vue'
import Home from './components/Home.vue'
import Login from './components/Login.vue'
import Logout from './components/Logout.vue'

import Support from './components/Support.vue'
import Forum from './components/Forum.vue'
import About from './components/About.vue'
import Download from './components/Download.vue'

import UserEdit from './components/Users/UserEdit.vue'
import UserDelete from './components/Users/UserDelete.vue'
import UserList from './components/Users/UserList.vue'

import Tchat from './components/Tchat.vue'

import Admin from './components/Admin.vue'
import NewsList from './components/News/NewsList.vue'
import NewsEdit from './components/News/NewsEdit.vue'


/*
import ClassList from './components/Classes/ClassList.vue'
import ClassEdit from './components/Classes/ClassEdit.vue'

import StudentList from './components/Students/StudentList.vue'
import StudentEdit from './components/Students/StudentEdit.vue'

import TeacherList from './components/Teachers/TeacherList.vue'
import TeacherEdit from './components/Teachers/TeacherEdit.vue'
import TeacherAssign from './components/Teachers/TeacherAssign.vue'
*/


//import FollowingList from './components/GitHub/FollowingList.vue'

import AuthService from './services/AuthService'

Vue.use(VueRouter)

moment.locale('fr');
Vue.filter('formatDate', function(value) {
  if (value) { 
    return moment(String(value)).format('lll')
  }
})

/**
 * Filter for routes requiring an authenticated user
 * @param {*} to 
 * @param {*} from 
 * @param {*} next 
 */
function requireAuth (to, from, next)  {
  if (!AuthService.isConnected) {
    next({
      path: '/login',
      query: { redirect: to.fullPath }
    });

    return;
  }

  var requiredProviders = to.meta.requiredProviders;

  if(requiredProviders && !AuthService.isBoundToProvider(requiredProviders)) {
    next( false )
  };

  next();
}
/**
 * Declaration of the different routes of our application, and the corresponding components
 */
const router = new VueRouter({
  mode: 'history',
  base: 'app',
  routes: [
    { path: '/login' , component: Login },
    { path: '/logout', component: Logout, beforeEnter: requireAuth },
    { path: '/support', component: Support },
    { path: '/forum', component: Forum },
    { path: '/about', component: About },
    { path: '/download', component: Download },
    { path: '/tchat', component: Tchat },

    { path: '/admin', component: Admin, beforeEnter: requireAuth },
    { path: '/news', component: NewsList, beforeEnter: requireAuth },
    { path: '/news/:mode([create|edit]+)/:id?', component: NewsEdit/*, beforeEnter: requireAuth*/ },

    { path: '/users', component: UserList, beforeEnter: requireAuth },

    { path: '/usersettings', component: UserEdit, beforeEnter: requireAuth },
    { path: '/deleteaccount', component: UserDelete, beforeEnter: requireAuth },
    
    //{ path: '/classes', component: ClassList/*, beforeEnter: requireAuth*/ },
    //{ path: '/classes/:mode([create|edit]+)/:id?', component: ClassEdit/*, beforeEnter: requireAuth*/ },

    //{ path: '/students', component: StudentList/*, beforeEnter: requireAuth*/ },
    //{ path: '/students/:mode([create|edit]+)/:id?', component: StudentEdit/*, beforeEnter: requireAuth*/ },

    //{ path: '/teachers', component: TeacherList/*, beforeEnter: requireAuth*/ },
    //{ path: '/teachers/:mode([create|edit]+)/:id?', component: TeacherEdit/*, beforeEnter: requireAuth*/ },
    //{ path: '/teachers/assign/:id', component: TeacherAssign/*, beforeEnter: requireAuth*/ },

    //{ path: '/github/following', component: FollowingList, beforeEnter: requireAuth, meta: { requiredProviders: ['GitHub'] } }, 

    { path: '', component: Home }
  ]
})

/**
 * Configuration of the authentication service
 */

// Allowed urls to access the application (if your website is http://mywebsite.com, you have to add it)
AuthService.allowedOrigins = ['http://localhost:5000', 'http://localhost:8080',/* 'http://mywebsite.com' */];

// Server-side endpoint to logout
AuthService.logoutEndpoint = '/Account/LogOff';

// Allowed providers to log in our application, and the corresponding server-side endpoints
AuthService.providers = {
  'Base': {
    endpoint: '/Account/Login'
  },
  'Google': {
    endpoint: '/Account/ExternalLogin?provider=Google'
  },
//INSERT FACEBOOK PROVIDER HERE
};

AuthService.appRedirect = () => router.replace('/');

// Creation of the root Vue of the application
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})