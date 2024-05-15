import { Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { PosComponent } from './pos/pos.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { AddfooditemComponent } from './addfooditem/addfooditem.component';
import { AddcategoryComponent } from './addCategoryc/addcategory/addcategory.component';
import { AddsupplierComponent } from './addsupplier/addsupplier/addsupplier.component';
import { CategoryComponent } from './addCategoryc/category/category.component';
import { EditCategorylComponent } from './addCategoryc/edit-categoryl/edit-categoryl.component';
import { SuppliersListComponent } from './addsupplier/suppliers-list/suppliers-list.component';
import { EditSupplierComponent } from './addsupplier/edit-supplier/edit-supplier.component';
import { AddInventoryitemComponent } from './InventoryItem/add-inventoryitem/add-inventoryitem.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { authgaurdGuard } from './Gaurd/authgaurd.guard';
import { InventoryitemListComponent } from './InventoryItem/inventoryitem-list/inventoryitem-list.component';
import { EditInventoryitemComponent } from './InventoryItem/edit-inventoryitem/edit-inventoryitem.component';

export const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'pos', component: PosComponent },
    {path:'addsupplier',component:AddsupplierComponent},
    {path:'category',component:CategoryComponent},
    { path: 'fooditem', component: AddfooditemComponent },
    {path:'addcategory',component:AddcategoryComponent},
    { path: 'editcategory/:id', component: EditCategorylComponent },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    {path:'editsupplier/:id',component:EditSupplierComponent},
    { path: 'supplierlist',component :SuppliersListComponent},
    {path:'addinventoryitem',component:AddInventoryitemComponent},
    {path: 'invlist', component :InventoryitemListComponent},
    {path:'edititem/:id',component:EditInventoryitemComponent},
    { path: 'admin', canActivate: [authgaurdGuard], component: AdminComponent },
    {path :'**',component:NotfoundComponent},



];
