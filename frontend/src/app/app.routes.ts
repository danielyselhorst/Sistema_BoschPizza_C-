import { Routes } from '@angular/router';
import { Login } from './features/auth/pages/login/login';
import { AccessDenied } from './features/auth/pages/access-denied/access-denied';
import { MainLayout } from './layout/components/main-layout/main-layout';
import { authGuard } from './core/guards/auth-guard';
import { PizzaList } from './features/pizzas/pages/pizza-list/pizza-list';
import { PizzaForm } from './features/pizzas/pages/pizza-form/pizza-form';
import { Home } from './features/dashboard/pages/home/home';
import { ClienteList } from './features/clientes/pages/cliente-list/cliente-list';
import { ClienteForm } from './features/clientes/pages/cliente-form/cliente-form';
import { Cadastro } from './features/auth/pages/cadastro/cadastro';

export const routes: Routes = [

    { path: '', redirectTo: 'login', pathMatch: 'full'},
    { path: 'login', component: Login},
    { path: 'cadastro', component: Cadastro },
    { path: 'acesso-negado', component: AccessDenied},

    {
        path: '',
        component: MainLayout,
        canActivate: [authGuard],
        children: [
            { path: 'home', component: Home},
            { path: 'pizzas', component: PizzaList},
            { path: 'pizzas/novo', component: PizzaForm},
            { path: 'pizzas/editar/:id', component: PizzaForm},
            { path: 'clientes', component: ClienteList},
            { path: 'clientes/novo', component: ClienteForm},
            { path: 'clientes/editar/:id', component: ClienteForm},
        ]
    },

    { path: '**', redirectTo: 'acesso-negado'},
];