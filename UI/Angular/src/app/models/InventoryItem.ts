import { Category } from "./category";
import { Supplier } from "./supplier";

export interface InventoryItem {
   id: number;
  name: string;
  description?: string;
  isAvailable?: number;
  imageUrl?: string;
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
