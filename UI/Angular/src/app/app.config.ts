import { ApplicationConfig, NgModule, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { BrowserModule, provideClientHydration }  
    from '@angular/platform-browser'; 
import { HttpClientModule } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [importProvidersFrom(BrowserModule), provideRouter(routes), provideClientHydration(), importProvidersFrom(HttpClientModule), importProvidersFrom(NgModule), provideAnimations()]
};
