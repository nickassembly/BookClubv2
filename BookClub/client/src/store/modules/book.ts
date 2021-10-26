import { bookService } from '../../services/book.service';
import { EventBus } from '../../event-bus';
import Vue from 'vue';

const state = {bookList: {}, status: '' };

const getters = {
    bookList: (bookState: any) => bookState,
};

const actions = {
    bookRequest: ({commit, dispatch}: {commit: any, dispatch: any}) => {
        commit('bookRequest');
        bookService.getBooks()
        .subscribe((result: any) => {
          commit('bookSuccess', result);
        },
      (errors: any) => {
        commit('bookError');
        dispatch('auth/authLogout', null, { root: true });
      });
    },
};

const mutations = {
  bookRequest: (bookState: any) => {
        bookState.status = 'attempting request for user book data';
    },
    bookSuccess: (bookState: any, bookResp: any) => {
      bookState.status = 'success';
      Vue.set(bookState, 'book', bookResp);
    },
    bookError: (bookState: any) => {
      bookState.status = 'error';
    },
};

export default {
    state,
    getters,
    actions,
    mutations,
};

