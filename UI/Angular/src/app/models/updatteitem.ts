import { Category } from "./category";
import { Supplier } from "./supplier";

export interface Inventory {
  id: string | number;
  name: string;
  description?: string;
  isAvailable?: number;
  imageUrl?: string;
  // categoryId?: number;
  // supplierId?: number;
  category?: Category;
  suppliers?: Supplier;
}
