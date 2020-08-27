export default [
  { // Always leave this as last one
    path: '/Connexion',
    component: () => import('layouts/connexion'),
    children: [
      { path: '/Login', component: () => import('pages/connexion/login') },
      { path: '/Inscription', component: () => import('pages/connexion/registration') }
    ]
  },
  {
    path: '/',
    component: () => import('layouts/default'),
    children: [
      { path: '/', component: () => import('pages/index') },
      { path: '/Account', component: () => import('pages/views/account') },
      { path: '/AccountType', name: 'accountType', component: () => import('pages/views/accountType') },
      { path: '/ActionCode', name: 'actionCode', component: () => import('pages/views/actionCode') },
      { path: '/CodeMapping', name: 'codeMapping', component: () => import('pages/views/codeMapping') },
    ]
  },

  { // Always leave this as last one
    path: '*',
    component: () => import('pages/404')
  },
  // { path: '/Account', name: 'account', component: () => import('pages/account') }
]
