export interface Category {
  id?: number;
  name: string;
  description?: string;
  imageUrl: string; // Ensure 'imgurl' is a required property
  isActive?: number;
}
