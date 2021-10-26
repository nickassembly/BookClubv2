import Vue from 'vue';
import Vuex from 'vuex';
import auth from './modules/auth';
import user from './modules/user';
import book from './modules/book';
import author from './modules/author';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {

  },
  mutations: {

  },
  actions: {

  },
  modules: {
    auth: {
      namespaced: true,
      state: auth.state,
      mutations: auth.mutations,
      getters: auth.getters,
      actions: auth.actions,
    },
    user: {
      namespaced: true,
      state: user.state,
      actions: user.actions,
      mutations: user.mutations,
      getters: user.getters,
    },
    book: {
      namespaced: true,
      state: book.state,
      actions: book.actions,
      mutations: book.mutations,
      getters: book.getters,
    },
    author: {
      namespaced: true,
      state: author.state,
      actions: author.actions,
      mutations: author.mutations,
      getters: author.getters,
    }
  }
});
