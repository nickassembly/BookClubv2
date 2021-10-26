import { profileService } from '../../services/profile.service';
import { EventBus } from '../../event-bus';
import Vue from 'vue';

const state = { profile: {}, status: '' };

const getters = {
    profile: (authorState: any) => authorState,
};

const actions = {
    authorRequest: ({commit, dispatch}: {commit: any, dispatch: any}) => {
        commit('authorRequest');
        profileService.get()
        .subscribe((result: any) => {
          commit('authorSuccess', result);
        },
      (errors: any) => {
        commit('authorError');
        dispatch('auth/authLogout', null, { root: true });
      });
    },
};

const mutations = {
  authorRequest: (authorState: any) => {
      authorState.status = 'attempting request for user profile data';
    },
    authorSuccess: (authorState: any, authorResp: any) => {
      authorState.status = 'success';
      Vue.set(authorState, 'profile', authorResp);
    },
    authorError: (authorState: any) => {
      authorState.status = 'error';
    },
};

export default {
    state,
    getters,
    actions,
    mutations,
};

