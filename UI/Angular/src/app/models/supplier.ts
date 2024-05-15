export interface Supplier {
  id: number;
  name: string;
  email?: string;
  phone?: string;
  // inventoryItems?: InventoryItem[]; // Many-to-many relationship
}
