import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {IPagination} from "../shared/models/pagination";
import {IBrand} from "../shared/models/brand";
import {IType} from "../shared/models/productTypes";
import {map} from "rxjs/operators";
import {ShopPrams} from "../shared/models/shopPrams";
import {IProduct} from "../shared/models/product";

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProducts(shopPrams: ShopPrams){
  let params = new HttpParams();

  if (shopPrams.brandId !== 0) {
    params = params.append('brandId', shopPrams.brandId.toString())
  }

  if (shopPrams.typeId !== 0) {
    params = params.append('typeId', shopPrams.typeId.toString())
  }

  if(shopPrams.search) {
    params = params.append('search', shopPrams.search);
  }

    params = params.append('sort', shopPrams.sort);
    params = params.append('pageIndex', shopPrams.pageNumber.toString());
    params = params.append('pageIndex', shopPrams.pageSize.toString());


    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      )
  }

  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
