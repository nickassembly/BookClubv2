<template>
  <div> 
    <section class="hero is-info">
      <div class="hero-body">
        <div class="container has-text-centered">
          <h1 class="title">User Book List</h1>
          <h2 class="subtitle">
            Welcome back!
          </h2>
        </div>
      </div>
    </section> 
    <section class="container">
      <Spinner v-bind:show="isBusy" />
      <b-container fluid>
        <b-row class="text-center" v-for="book in homeData.books" :key="book.bookId" >
          <a href="#" :key="book.id" @click="get_external_data(book.title)">
          <div class="book-item card">          
            <div class="card-content">
                <b-col v-if="book.title"><h2><strong>{{ book.title }}</strong></h2></b-col>
                <b-col>{{ book.description }}</b-col>
                <b-col>{{ book.category }}</b-col>
                <b-row class="text-center" v-for="author in book.authors" :key="author.authorId" >
                  <div class="card" >          
                    <div class="card-content">
                      <b-col v-if="author.firstname">{{ author.firstname }} {{ author.lastname }}</b-col>
                      <b-col>{{ book.description }}</b-col>
                      <b-col>{{ book.category }}</b-col>
                    </div>
                  </div>
                </b-row>
            </div>
          </div>
          </a>
        </b-row>
        <b-row class="text-center">
          <b-col cols="12">
            <button class="favorite styled" type="button" label="Add Book" content="Add Book" :click="addNewBook()">Add Book</button>
          </b-col>
        </b-row>
      </b-container>
    </section>
  <section>
  <div v-if="showBookDetails">
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <div class="modal-title">Books found</div>
                <button type="button" class="close" @click="showBookDetails=false">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <b-container fluid>
                  <b-row class="text-center" v-for="googleBook in bookData.items" :key="googleBook.id" >
                    <div class="book-item card">          
                      <div class="card-content">
                        <a target="_blank" v-bind:href="'https://books.google.com/books?id='+googleBook.id+'&printsec=frontcover&source=gbs_ViewAPI'">
                          <img style="float:left;" src="../../assets/gbs_preview_button1.gif" />
                        </a>
                        <b-col v-if="googleBook.volumeInfo"><h2><strong>{{ googleBook.volumeInfo.title }}</strong></h2></b-col>
                        <b-col>{{ googleBook.volumeInfo.description }}</b-col>
                        <b-row class="text-center" v-for="author in googleBook.volumeInfo.authors" :key="author.authorId" >
                          <b-col>Author: {{ author }}</b-col>
                        </b-row>
                      </div>
                    </div>
                  </b-row>
                </b-container>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
  </section>
  </div>
</template>

<script lang="ts">
  import Spinner from '@/components/Spinner.vue'; // @ is an alias to /src
  import { Component, Vue } from 'vue-property-decorator';
  import { mapGetters } from 'vuex';
  import { dashboardService } from '../../services/dashboard.service';
  import { bookService } from '../../services/book.service';

  @Component({
    computed: mapGetters({
      profile: 'user/profile',
    }),
    components: {
      Spinner
    }
  })

  export default class UserBookList extends Vue {
    private isBusy: boolean = false;
    private homeData = {} as any;
    private bookData = {} as any;
    private showBookDetails = false as boolean;
    private showAddBookModal = false as boolean;
    
    private created() {
      this.isBusy = true;
      dashboardService.getHomeDetails().then((resp: any) => {
        this.homeData = resp.data;
          this.isBusy = false;
      })
    }
    private addNewBook() {
      this.showAddBookModal = true;
    }

    private get_external_data(bookTitle: string) {
      bookService.getBookDetails(bookTitle).then((resp: any) => {
        this.bookData = resp.data;
        console.log(resp.data);
        this.showBookDetails = true;
        this.isBusy = false;
      })
    }

  }

</script>

<style scoped>
  .book-item {
    margin: 10px;
    background-color: lightgray;
  }

  .modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, .5);
    display: table;
    transition: opacity .3s ease;
  }

  .modal-wrapper {
    display: table-cell;
    vertical-align: middle;
  }

  .modal-header {
    position: fixed;
    z-index: 10;
    background-color: white;
    width:inherit;
  }

  .modal-body {
    margin-top: 50px;
  }

  .modal-title {
    text-align: center;
  }

  .add-book-button {
    width: 100px;
    height: 50px;
  }
  .styled {
    width: 150px;
    height: 50px;
    border: 0;
    line-height: 2.5;
    padding: 0 20px;
    font-size: 1rem;
    text-align: center;
    color: #fff;
    text-shadow: 1px 1px 1px #000;
    border-radius: 10px;
    background-color: rgba(0, 0, 220, 1);
    background-image: linear-gradient(to top left,
                                      rgba(0, 0, 0, .2),
                                      rgba(0, 0, 0, .2) 30%,
                                      rgba(0, 0, 0, 0));
    box-shadow: inset 2px 2px 3px rgba(255, 255, 255, .6),
                inset -2px -2px 3px rgba(0, 0, 0, .6);
    color: #fff;
  }

  .styled:hover {
    background-color: rgba(0, 0, 255, 1);
  }

  .styled:active {
    box-shadow: inset -2px -2px 3px rgba(255, 255, 255, .6),
                inset 2px 2px 3px rgba(0, 0, 0, .6);
  }

</style>
