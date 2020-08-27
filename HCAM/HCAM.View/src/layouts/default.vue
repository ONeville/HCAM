<template>
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar color="indigo">
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen">
          <q-icon name="menu" />
        </q-btn>

        <q-toolbar-title>
          HCA
          <div slot="subtitle">Management</div>
        </q-toolbar-title>
      </q-toolbar>
    </q-layout-header>

    <q-layout-drawer
      v-model="leftDrawerOpen"
      content-class="bg-grey-2"
    >
      <q-list no-border
        link inset-delimiter>

        <q-list-header>Essential</q-list-header>
        <q-item v-for="(item, i) in items" :key="i" :to="item.link" separator>
          <q-item-side :icon="item.icon" />
          <q-item-main :label="item.text"  />
        </q-item>
      </q-list>

      <q-list separator>
        <q-list-header>Configurations</q-list-header>
        <q-collapsible indent :icon="item.icon" :label="item.text" v-for="(item, y) in configs" :key="y">
          
        <q-item v-for="(child, z) in item.children" :key="z" :to="child.link" separator>
          <q-item-side :icon="child.icon" />
          <q-item-main :label="child.text" :sublabel="item.subText" />
        </q-item>
          
        </q-collapsible>
      </q-list>

    </q-layout-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { openURL } from 'quasar'

export default {
  name: 'LayoutDefault',
  data () {
    return {
      leftDrawerOpen: this.$q.platform.is.desktop,
      lorem: 'Lorem ipsum dolor sit amet',
      drawer: null,
      mtoggle: false,
      miniVariant: true,
      items: [
          { icon: "contacts", text: "Home", link: "/", subText: "main entry" },
          { icon: "dashboard", text: "Dashboard", link: "dashboard", subText: "reportings" },
          {
            icon: "account_balance_wallet",
            text: "Journal Entry",
            link: "journalEntry", subText: "entry reports"
          },
          {
            icon: "monetization_on",
            text: "Budget Postings",
            link: "budgetPostings", subText: "posting and details"
          }
        ],
      configs:[
      {
            icon: "dns",
            text: "Settings",
            link: "#",
            children: [
              { icon: "swap_horiz", text: "Code Mapping", link: "codeMapping", toggle: false  }
            ]
      },
      {
            icon: "settings",
            text: "Configurations",
            link: "#",
            children: [
              { icon: "account_balance", text: "Accounts", link: "account", toggle: false  },
              { icon: "payment", text: "Account types", link: "accountType", toggle: false  },
              { icon: "group_work", text: "Action Codes", link: "actionCode", toggle: false }
            ]
      }],
    }
  },
  methods: {
    openURL,
    setTime(){

    },
  }
}
</script>

<style>
</style>
