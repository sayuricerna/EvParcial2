import { Routes } from '@angular/router';
import { ListaComponent as ProductosLista }     from './components/productos/lista/lista.component/lista.component';
import { FormularioComponent as ProductosForm } from './components/productos/formulario/formulario.component/formulario.component';
import { ListaComponent as ClientesLista }      from './components/clientes/lista/lista.component//lista.component';
import { FormularioComponent as ClientesForm }  from './components/clientes/formulario/formulario.component/formulario.component';
import { ListaComponent as CarritosLista }      from './components/carritos/lista/lista.component/lista.component';
import { DetalleComponent }                     from './components/carritos/detalle/detalle.component/detalle.component';
import { ListaComponent as ComprasLista }       from './components/compras/lista/lista.component/lista.component';

export const routes: Routes = [
  { path: '',                     redirectTo: '/productos', pathMatch: 'full' },
  { path: 'productos',            component: ProductosLista },
  { path: 'productos/nuevo',      component: ProductosForm },
  { path: 'productos/editar/:id', component: ProductosForm },
  { path: 'clientes',             component: ClientesLista },
  { path: 'clientes/nuevo',       component: ClientesForm },
  { path: 'clientes/editar/:id',  component: ClientesForm },
  { path: 'carritos',             component: CarritosLista },
  { path: 'carritos/:id',         component: DetalleComponent },
  { path: 'compras',              component: ComprasLista },
];