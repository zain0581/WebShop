import { TestBed } from '@angular/core/testing';

import { AddcategoryService } from './addcategory.service';

describe('AddcategoryService', () => {
  let service: AddcategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddcategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
