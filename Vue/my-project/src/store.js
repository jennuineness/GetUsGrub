import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    MINIMUM_MENU_ITEM_PRICE: 0.01,
    MAX_MENU_ITEM_PRICE: 1000,
    MenuItems: [
      //{
      //  menuItemName: 'Big Mac',
      //  menuItemPrice: 4.00
      //},
      //{
      //  menuItemName: 'Large Fries',
      //  menuItemPrice: 2.50
      //},
      //{
      //  menuItemName: 'McFlurry',
      //  menuItemPrice: 1.00
      //},
      //{
      //  menuItemName: 'Large Soft Drink',
      //  menuItemPrice: 2.00
      //},
      //{
      //  menuItemName: 'McChicken',
      //  menuItemPrice: 1.50
      //},
      //{
      //  menuItemName: 'Water Bottle',
      //  menuItemPrice: 10.50
      //}
    ],
    BillItems: [
    ]
  },
  getters: {
    totalPrice: state => {
      var totalPrice = this.$store.state.BillItems.map(BillItems => {
        return state.BillItems.menuItemPrice;        
      })
    }
  },
  mutations: {
    AddToDictionary: (state, payload) => {
      state.MenuItems.push({
        menuItemName: payload[0],
        menuItemPrice: payload[1]
      });
    },

    RemoveFromDictionary: (state, payload) => {
      console.log("Store Mutation: " +payload)
      state.MenuItems.splice(payload, 1)
    },
    RemoveFromBill: (state, payload) => {
      console.log("Store Mutation: " +payload)
      state.BillItems.splice(payload, 1)
    }
  },
  // Actions are necessary when performing asynchronous methods.
  actions: {
    AddToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log("Added Food Item Name: " +payload[0]);
        console.log("Added Food Item Price: " +payload[1]);
        context.commit('AddToDictionary', payload)
      }, 500)
    },
    RemoveFromDictionary: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromDictionary', payload)
      }, 500)
    },
    RemoveFromBill: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromBill', payload)
      }, 500)
    }
  }
})

//export default new Vuex.Store({
//  state: {
//    isAuthenticated: false
//  },
//  getters: {
//    isAuth: function (state) {
//      return state.isAuthenticated
//    }
//  },
//  mutations: {
//    signIn: function (state)_{
//      state.isAuthenticated = true
//    }
//  },
//  actions: {
//    signIn: function (context, payload) {
//      context.commit('signIn')
//    }
//  }
//})
