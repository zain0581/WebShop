import { Category } from "./category";
import { Supplier } from "./supplier";

export interface InventoryItem {
   id: number;
  name: string;
  description?: string;
  qty?: number;
  imageUrl?: string;
  unitPrice?: number;
  // categoryId?: number;
  // supplierId?: number;
  category?: Category;
  suppliers?: Supplier;



}
// export interface CategoryDto {
//   id: number;
//   name: string;
//   description?: string;
//   imageUrl?: string;
//   isActive?: number;
// }
// export interface SupplierDto {
//   id: number;
//   name: string;
//   email?: string;
//   phone?: string;
// }
