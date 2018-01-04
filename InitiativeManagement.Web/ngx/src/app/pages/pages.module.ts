import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PagesRoutes } from './pages.routing';

import { RegisterComponent } from './register/register.component';
import { LockComponent } from './lock/lock.component';
import { LoginComponent } from './login/login.component';
import { DxButtonModule, DxTextBoxModule, DxValidationGroupModule, DxValidatorModule } from 'devextreme-angular';

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(PagesRoutes),
        FormsModule,
        ReactiveFormsModule,
        DxButtonModule,
        DxTextBoxModule,
        DxValidationGroupModule,
        DxValidatorModule
    ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        LockComponent
    ]
})

export class PagesModule {}
