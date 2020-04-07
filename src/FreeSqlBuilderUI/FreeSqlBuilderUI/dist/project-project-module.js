(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["project-project-module"],{

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/builderOption/builderOption.component.html":
/*!*********************************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/builderOption/builderOption.component.html ***!
  \*********************************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<form nz-form [formGroup]=\"validateForm\">\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>模块名</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"name\" formControlName=\"name\" placeholder=\"模块名\" />\n    </nz-form-control>\n  </div>\n  <!-- <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>是否只生成主表</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <nz-switch id=\"isServiceOnly\" formControlName='isServiceOnly'>\n      </nz-switch>\n    </nz-form-control>\n  </div> -->\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\">前缀</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"prefix\" formControlName='prefix' placeholder=\"前缀 ex: I -> 文件名= I+实体名+后缀\" />\n    </nz-form-control>\n  </div>\n  <!-- <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\">忽略数据库表中的前缀的</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"splitDot\" formControlName='splitDot' placeholder=\"忽略数据库表中的前缀的 前缀分隔符 （与IsIgnorePrefix搭配）\" />\n    </nz-form-control>\n  </div>\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>名称是否忽略前缀</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <nz-switch id=\"isIgnorePrefix\" formControlName='isIgnorePrefix'>\n      </nz-switch>\n    </nz-form-control>\n  </div> -->\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>输出路径</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"outPutPath\" formControlName=\"outPutPath\" placeholder=\"输出路径\" />\n    </nz-form-control>\n  </div>\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>名称转换模式</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <nz-select formControlName=\"mode\" nzAllowClear nzPlaceHolder=\"名称转换模式 默认不转换\">\n        <nz-option nzValue=0 nzLabel=\"不变\"></nz-option>\n        <nz-option nzValue=1 nzLabel=\"全大写\"></nz-option>\n        <nz-option nzValue=2 nzLabel=\"全小写\"></nz-option>\n        <nz-option nzValue=3 nzLabel=\"首字母大写\"></nz-option>\n      </nz-select>\n    </nz-form-control>\n  </div>\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>模板路径</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <button nz-button type=\"button\" (click)=\"selectTemplate()\">{{TemplateButton()}}</button>\n      <!-- <label>{{this.builder.template.templateName}}</label> -->\n    </nz-form-control>\n  </div>\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\">后缀</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"suffix\" formControlName=\"suffix\" placeholder=\"后缀 ex:Repository-> 文件名= 前缀+实体名+Repository\" />\n    </nz-form-control>\n  </div>\n  <div nz-col [nzSpan]=\"12\">\n    <nz-form-label [nzSm]=\"5\" [nzXs]=\"24\" nzRequired>文件后缀</nz-form-label>\n    <nz-form-control [nzSm]=\"9\" [nzXs]=\"24\">\n      <input nz-input id=\"fileExtensions\" formControlName=\"fileExtensions\" placeholder=\"文件后缀\" />\n    </nz-form-control>\n  </div>\n  <nz-form-control [nzSm]=\"12\" [nzXs]=\"24\">\n    <nz-form-control [nzOffset]=\"5\" [nzSm]=\"9\" [nzXs]=\"24\">\n      <button nz-button (click)='submitForm()' [nzType]=\"'primary'\">提交</button>\n    </nz-form-control>\n  </nz-form-control>\n  <nz-modal nzWidth=\"80vw\" [(nzVisible)]=\" this.templateSel \" nzTitle=\"选择模板\" (nzOnOk)=\"this.templateSel = false\"\n    (nzOnCancel)=\"this.templateSel = false\">\n    <app-list-template [isController]='true' (selectTemplate)=\"selectCallBack($event)\"></app-list-template>\n  </nz-modal>\n</form>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/newproject.component.html":
/*!****************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/newproject.component.html ***!
  \****************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<nz-steps [nzCurrent]=\"current\" nzSize=\"default\">\n  <nz-step nzTitle=\"项目基本信息填写\"></nz-step>\n  <nz-step nzTitle=\"模式选择\"></nz-step>\n  <nz-step nzTitle=\"表构建器参数填写\"></nz-step>\n  <nz-step nzTitle=\"全表构建器参数填写\"></nz-step>\n  <nz-step nzTitle=\"生成\"></nz-step>\n  <nz-step nzTitle=\"查看代码\"></nz-step>\n</nz-steps>\n<div class=\"steps-content\">\n  <app-project-info [projectId]=\"this.projectId\" (callBack)=\"callBack($event)\" *ngIf=\"current === 0\">\n  </app-project-info>\n  <app-generator-mode [project]=\"this.project\" (callBack)=\"callBack($event)\" *ngIf=\"current === 1\">\n  </app-generator-mode>\n  <app-builder-container *ngIf=\"current === 2\" (callBack)=\"builderCallBack($event)\" [builders]=\"this.project.builders\"\n    [projectid]=\"this.projectId\" [type]=\"0\">\n  </app-builder-container>\n  <app-builder-container *ngIf=\"current === 3\" [builders]=\"this.project.globalBuilders\" [projectid]=\"this.projectId\"\n    [type]=\"1\">\n  </app-builder-container>\n  <app-build-task (callBack)=\"IsFinish($event)\" *ngIf=\"current === 4\" [projectid]=\"this.projectId\">\n  </app-build-task>\n  <div *ngIf=\"current === 5\">\n    <h3>生成成功 生成路径为：{{this.getCodePath()}}</h3>\n  </div>\n</div>\n<div nz-col class=\"steps-action\">\n  <nz-button-group [nzSize]=\"large\">\n    <button nz-button nzType=\"default\" (click)=\"pre()\" *ngIf=\"current > 0 && current !== 5\"><i nz-icon\n        nzType=\"left\"></i>上一步</button>\n    <button nz-button nzType=\"primary\" (click)=\"next()\" *ngIf=\"this.currentFinish[this.current] &&current < 6\">下一步<i\n        nz-icon nzType=\"right\"></i></button>\n    <button nz-button nzType=\"primary\" [nzSize]=\"large\" class=\"stepbtn\" (click)=\"finished()\" *ngIf=\"current === 5\"><i\n        nz-icon nzType=\"check\" nzTheme=\"outline\"></i>完成 </button>\n  </nz-button-group>\n\n</div>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/project.component.html":
/*!**************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/project/project.component.html ***!
  \**************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<div>\n  <div nz-row [nzGutter]=\"30\" nzType='flex' nzJustify=\"start\">\n    <div nz-col nzXs=\"24\" nzSm=\"12\" nzMd=\"8\" nzLg=\"6\" nzXl=\"4\">\n      <nz-card nzTitle=\"新建项目\" nzHoverable [nzCover]=\"addProject\" (click)=\"newProject()\">\n        <nz-card-meta nzTitle=\"新建\" nzDescription=\"添加一个新的项目\"></nz-card-meta>\n      </nz-card>\n      <ng-template #addProject>\n        <img alt=\" 添加项目\" src=\"./assets/images/plus.png\" />\n      </ng-template>\n    </div>\n    <div nz-col nzXs=\"24\" nzSm=\"12\" nzMd=\"8\" nzLg=\"6\" nzXl=\"4\">\n      <nz-card nzTitle=\"查看所有项目\" nzHoverable [nzCover]=\"listTemplate\" (click)=\"projectList()\">\n        <nz-card-meta nzTitle=\"查看\" nzDescription=\"所有项目列表\"></nz-card-meta>\n      </nz-card>\n      <ng-template #listTemplate>\n        <img alt=\"查看所有项目\" src=\"./assets/images/list.png\" />\n      </ng-template>\n    </div>\n  </div>\n</div>\n");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/projectList/projectList.component.html":
/*!******************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/project/projectList/projectList.component.html ***!
  \******************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<nz-table #basicTable [nzTotal]=\"pageData.total\" [(nzPageIndex)]=\"pageData.pageNumber\"\n  [(nzPageSize)]=\"pageData.pageSize\" [nzFrontPagination]=\"false\" [nzShowPagination]=\"true\" nzShowSizeChanger nzShowTotal\n  [nzLoading]=\"loading\" nzTitle=\"项目列表\" (nzPageIndexChange)=\"IndeChange($event)\" (nzPageSizeChange)=\"SizeChange($event)\"\n  [nzData]=\"pageData.datas\">\n  <thead>\n    <tr>\n      <th>项目名称</th>\n      <th>生成模式</th>\n      <th>作者</th>\n      <th>操作</th>\n    </tr>\n  </thead>\n  <tbody>\n    <tr *ngFor=\"let data of basicTable.data\">\n      <td>{{data.projectInfo.projectName}}</td>\n      <td>{{renderGeneratorMode(data)}}</td>\n      <td>{{data.projectInfo.author}}</td>\n      <td>\n        <a (click)=\"build(data)\"\n          *ngIf=\"data.projectInfo.id !== 0 && data.generatorModeConfigId !== 0 && data.builders.length > 0\">生成</a>\n        <nz-divider nzType=\"vertical\"></nz-divider>\n        <a (click)=\"gotoEdit(data.id)\">编辑</a>\n        <nz-divider nzType=\"vertical\"></nz-divider>\n        <a (click)=\"delproj(data.id)\">删除</a>\n      </td>\n    </tr>\n  </tbody>\n</nz-table>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/tablePreview/tablePreview.component.html":
/*!********************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/project/tablePreview/tablePreview.component.html ***!
  \********************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<nz-table #dynamicTable [nzScroll]=\"{ y: '500px' }\" nzBordered=\"true\" [nzData]=\"listOfData\">\n  <thead>\n    <tr>\n      <th nzWidth=\"100px\">属性</th>\n      <th nzWidth=\"8vw\">表名</th>\n      <th nzWidth=\"8vw\">名称</th>\n      <th nzWidth=\"10vw\">备注</th>\n      <th nzWidth=\"8vw\">主键</th>\n      <th nzRight=\"0px\">操作</th>\n    </tr>\n  </thead>\n  <tbody>\n    <ng-template ngFor let-data [ngForOf]=\"dynamicTable.data\">\n      <tr>\n        <td nzBreakWord nzLeft=\"0px\" nzShowExpand [(nzExpand)]=\"mapOfExpandData[data.csName]\"></td>\n        <td nzBreakWord nzLeft=\"100px\">{{ data.dbName }}</td>\n        <td nzBreakWord>{{ data.csName }}</td>\n        <td nzBreakWord>{{ data.comment }}</td>\n        <td nzBreakWord>{{ getPkName(data)}}</td>\n        <td nzBreakWord nzRight=\"0px\">\n          <a (click)=\"ignoreTable(data)\">{{data.isIgnore?'恢复':'忽略'}}</a>\n        </td>\n      </tr>\n      <tr [nzExpand]=\"mapOfExpandData[data.csName]\">\n        <td></td>\n        <td colspan=\"5\">\n          <nz-card [nzBordered]=\"false\" nzTitle=\"表属性\">\n            <p *ngFor=\"let item of data.columnsByCs\">\n              <label class=\"lblCol\">名称:{{item.value.csName}} </label>\n              <nz-divider nzType=\"vertical\"></nz-divider>\n              <label class=\"lblCol\">数据库类型: {{ item.value.dbTypeText}}</label>\n              <nz-divider nzType=\"vertical\"></nz-divider>\n            </p>\n          </nz-card>\n          <!-- <nz-card [nzBordered]=\"false\" [nzTitle]=\"'导航属性'\">\n            <p *ngFor=\"let item of data.navigateInfos\">\n              <label class=\"lblCol\">名称:{{item.columnName}} </label>\n              <nz-divider nzType=\"vertical\"></nz-divider>\n              <label class=\"lblCol\">导航类型:{{ item.csType }}</label>\n              <nz-divider nzType=\"vertical\"></nz-divider>\n            </p>\n          </nz-card> -->\n        </td>\n      </tr>\n    </ng-template>\n  </tbody>\n</nz-table>");

/***/ }),

/***/ "./src/app/project/modals/generatormodeconfig.ts":
/*!*******************************************************!*\
  !*** ./src/app/project/modals/generatormodeconfig.ts ***!
  \*******************************************************/
/*! exports provided: DataSource, GeneratorModeConfig, TableInfo, ColumnInfo, NavigateColumnInfo, NavigateCategory, GeneratorMode, DataType */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DataSource", function() { return DataSource; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorModeConfig", function() { return GeneratorModeConfig; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TableInfo", function() { return TableInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ColumnInfo", function() { return ColumnInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NavigateColumnInfo", function() { return NavigateColumnInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NavigateCategory", function() { return NavigateCategory; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorMode", function() { return GeneratorMode; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DataType", function() { return DataType; });
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};
var DataSource = /** @class */ (function () {
    function DataSource() {
    }
    return DataSource;
}());

var GeneratorModeConfig = /** @class */ (function () {
    function GeneratorModeConfig(projectid) {
        this.generatorMode = 1;
        this.dataSource = new DataSource();
        this.projectId = projectid;
        this.generatorMode = 1;
        this.id = 0;
        this.entityAssemblyName = '';
        this.entityBaseName = '';
        this.ignoreTables = '';
        this.includeTables = '';
    }
    return GeneratorModeConfig;
}());

var TableInfo = /** @class */ (function () {
    function TableInfo() {
        this.columnInfos = new Array();
        this.navigateInfos = new Array();
    }
    return TableInfo;
}());

var ColumnInfo = /** @class */ (function () {
    function ColumnInfo() {
    }
    return ColumnInfo;
}());

// tslint:disable-next-line: no-use-before-declare
var NavigateColumnInfo = /** @class */ (function (_super) {
    __extends(NavigateColumnInfo, _super);
    function NavigateColumnInfo() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return NavigateColumnInfo;
}(ColumnInfo));

var NavigateCategory;
(function (NavigateCategory) {
    NavigateCategory[NavigateCategory["OneToOne"] = 0] = "OneToOne";
    NavigateCategory[NavigateCategory["OneToMany"] = 1] = "OneToMany";
    NavigateCategory[NavigateCategory["None"] = 2] = "None";
})(NavigateCategory || (NavigateCategory = {}));
var GeneratorMode;
(function (GeneratorMode) {
    GeneratorMode[GeneratorMode["DbFirst"] = 0] = "DbFirst";
    GeneratorMode[GeneratorMode["CodeFirst"] = 1] = "CodeFirst";
})(GeneratorMode || (GeneratorMode = {}));
var DataType;
(function (DataType) {
    DataType[DataType["MySql"] = 0] = "MySql";
    DataType[DataType["SqlServer"] = 1] = "SqlServer";
    DataType[DataType["PostgreSQL"] = 2] = "PostgreSQL";
    DataType[DataType["Oracle"] = 3] = "Oracle";
    DataType[DataType["Sqlite"] = 4] = "Sqlite";
    DataType[DataType["OdbcOracle"] = 5] = "OdbcOracle";
    DataType[DataType["OdbcSqlServer"] = 6] = "OdbcSqlServer";
    DataType[DataType["OdbcMySql"] = 7] = "OdbcMySql";
    DataType[DataType["OdbcPostgreSQL"] = 8] = "OdbcPostgreSQL";
    DataType[DataType["Odbc"] = 9] = "Odbc";
    DataType[DataType["OdbcDameng"] = 10] = "OdbcDameng";
    DataType[DataType["MsAccess"] = 11] = "MsAccess";
})(DataType || (DataType = {}));


/***/ }),

/***/ "./src/app/project/modals/project.ts":
/*!*******************************************!*\
  !*** ./src/app/project/modals/project.ts ***!
  \*******************************************/
/*! exports provided: Project, BuilderOptions, BuilderType, ConvertMode */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Project", function() { return Project; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderOptions", function() { return BuilderOptions; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderType", function() { return BuilderType; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConvertMode", function() { return ConvertMode; });
/* harmony import */ var _generatormodeconfig__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./generatormodeconfig */ "./src/app/project/modals/generatormodeconfig.ts");
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};

var Project = /** @class */ (function () {
    function Project() {
        this.generatorModeConfig = new _generatormodeconfig__WEBPACK_IMPORTED_MODULE_0__["GeneratorModeConfig"](this.projectInfoId);
        this.builders = new Array();
    }
    return Project;
}());

var BuilderOptions = /** @class */ (function () {
    function BuilderOptions(_type, _name) {
        this.id = 0;
        this.projectId = 0;
        this.project = null;
        this.splitDot = '_';
        this.suffix = '';
        this.templateId = 0;
        this.type = _type;
        this.isServiceOnly = false;
        this.mode = 0;
        this.name = _name;
        this.outPutPath = '';
        this.fileExtensions = 'cs';
        this.classBase = '';
        this.prefix = '';
        this.isIgnorePrefix = true;
    }
    return BuilderOptions;
}());

var BuilderType;
(function (BuilderType) {
    BuilderType[BuilderType["Builder"] = 0] = "Builder";
    BuilderType[BuilderType["GlobalBuilder"] = 1] = "GlobalBuilder";
})(BuilderType || (BuilderType = {}));
var ConvertMode;
(function (ConvertMode) {
    ConvertMode[ConvertMode["None"] = 0] = "None";
    ConvertMode[ConvertMode["AllLower"] = 1] = "AllLower";
    ConvertMode[ConvertMode["AllUpper"] = 2] = "AllUpper";
    ConvertMode[ConvertMode["FirstUpper"] = 3] = "FirstUpper";
})(ConvertMode || (ConvertMode = {}));


/***/ }),

/***/ "./src/app/project/modals/projectInfo.ts":
/*!***********************************************!*\
  !*** ./src/app/project/modals/projectInfo.ts ***!
  \***********************************************/
/*! exports provided: ProjectInfo */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectInfo", function() { return ProjectInfo; });
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};
var ProjectInfo = /** @class */ (function () {
    function ProjectInfo() {
    }
    return ProjectInfo;
}());



/***/ }),

/***/ "./src/app/project/newproject/builderOption/BuilderContainer.component.ts":
/*!********************************************************************************!*\
  !*** ./src/app/project/newproject/builderOption/BuilderContainer.component.ts ***!
  \********************************************************************************/
/*! exports provided: BuilderContainerComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderContainerComponent", function() { return BuilderContainerComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _modals_project__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../modals/project */ "./src/app/project/modals/project.ts");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};




var BuilderContainerComponent = /** @class */ (function () {
    function BuilderContainerComponent(modalService, message, client) {
        this.modalService = modalService;
        this.message = message;
        this.client = client;
        this.projectid = 0;
        this.builders = new Array();
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.isVisible = false;
        this.tag = '';
        console.log(this.builders, "constructor builders");
    }
    BuilderContainerComponent.prototype.ngOnChanges = function (changes) {
        this.builders = changes['builders']['currentValue'];
        this.projectid = changes['projectid']['currentValue'];
        console.log(this.builders, 'ngOnChanges');
    };
    BuilderContainerComponent.prototype.ngOnInit = function () {
    };
    BuilderContainerComponent.prototype.removeBuilder = function (currentBuilder) {
        var _this = this;
        this.modalService.confirm({
            nzTitle: '删除构建器',
            nzContent: "<b style=\"color: red;\">\u662F\u5426\u8981\u5220\u9664 " + currentBuilder.name + " \u6784\u5EFA\u5668\u7684\u914D\u7F6E\uFF1F</b>",
            nzOkText: '是',
            nzOkType: 'danger',
            nzOnOk: function () { return _this.remove(currentBuilder); },
            nzCancelText: '否',
            nzOnCancel: function () { return console.log('Cancel'); }
        });
    };
    BuilderContainerComponent.prototype.remove = function (currentBuilder) {
        var _this = this;
        if (currentBuilder.id && currentBuilder.id !== 0) {
            this.client.delete("/api/project/builder/" + currentBuilder.id).subscribe(function (res) {
                if (res) {
                    _this.message.success('删除成功');
                    return;
                }
            });
        }
        this.builders.splice(this.builders.findIndex(function (b) { return b.name === currentBuilder.name; }), 1);
        return;
    };
    BuilderContainerComponent.prototype.Add = function () {
        this.isVisible = true;
    };
    BuilderContainerComponent.prototype.CallBack = function (e) {
        if (this.builders.filter(function (x) { return x.name === e.name; }).length > 0) {
            this.builders.filter(function (x) { return x.name === e.name; })[0].id = e.id;
        }
        this.callBack.emit(this.builders);
    };
    BuilderContainerComponent.prototype.builderAdd = function () {
        this.builders.push(new _modals_project__WEBPACK_IMPORTED_MODULE_1__["BuilderOptions"](this.type, this.tag));
        this.isVisible = false;
    };
    BuilderContainerComponent.ctorParameters = function () { return [
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"] },
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], BuilderContainerComponent.prototype, "projectid", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], BuilderContainerComponent.prototype, "builders", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], BuilderContainerComponent.prototype, "type", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], BuilderContainerComponent.prototype, "callBack", void 0);
    BuilderContainerComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-builder-container',
            template: "\n    <nz-card style=\"width: 100%;\" nzTitle=\"\u914D\u7F6E\u6784\u5EFA\u5668\" [nzExtra]=\"builderExtraTemplate\">\n      <nz-card-tab>\n        <nz-tabset  style='margin-bottom: 0px !important'>\n          <nz-tab  [nzTitle]=\"buildtitleTemplate\" *ngFor=\"let item of this.builders \">\n            <ng-template #buildtitleTemplate>\n              <div>\n                {{ item.name }}\n                <i nz-icon nzType=\"close\" (click)=\"removeBuilder(item)\" class=\"ant-tabs-close-x\"></i>\n              </div>\n            </ng-template>\n            <app-builder-option (callBack)=\"CallBack($event)\"  [projectid]=\"this.projectid\" *ngIf=\"item\" [builder]=\"item\">\n            </app-builder-option>\n          </nz-tab>\n        </nz-tabset>\n      </nz-card-tab>\n    </nz-card>\n    <ng-template #builderExtraTemplate>\n      <a (click)=\"Add()\">\u6DFB\u52A0</a>\n    </ng-template>\n    <nz-modal [(nzVisible)]=\"isVisible\" nzTitle=\"\u6DFB\u52A0\u6784\u5EFA\u5668\" (nzOnOk)=\"builderAdd()\"\n      (nzOnCancel)=\"this.isVisible = false\">\n      \u6784\u5EFA\u5668\u540D\uFF1A\n      <input nz-input id='key' [(ngModel)]=\"tag\">\n    </nz-modal>\n  ",
            styles: ["\n  "]
        }),
        __metadata("design:paramtypes", [ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"]])
    ], BuilderContainerComponent);
    return BuilderContainerComponent;
}());



/***/ }),

/***/ "./src/app/project/newproject/builderOption/builderOption.component.ts":
/*!*****************************************************************************!*\
  !*** ./src/app/project/newproject/builderOption/builderOption.component.ts ***!
  \*****************************************************************************/
/*! exports provided: BuilderOptionComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuilderOptionComponent", function() { return BuilderOptionComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _modals_project__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../modals/project */ "./src/app/project/modals/project.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var src_app_template_modals_template__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/template/modals/template */ "./src/app/template/modals/template.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};






var BuilderOptionComponent = /** @class */ (function () {
    function BuilderOptionComponent(fb, client, message) {
        this.fb = fb;
        this.client = client;
        this.message = message;
        this.projectid = 0;
        this.builder = new _modals_project__WEBPACK_IMPORTED_MODULE_2__["BuilderOptions"](_modals_project__WEBPACK_IMPORTED_MODULE_2__["BuilderType"].Builder, 'Entity');
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    BuilderOptionComponent.prototype.ngOnChanges = function (changes) {
        this.projectid = changes['projectid']['currentValue'];
        this.builder = changes['builder']['currentValue'];
        console.log(this.builder, 'ngOnChanges');
        if (!this.projectid || this.projectid === 0) {
            this.message.warning('未检测到项目');
        }
        if (!this.builder || this.builder.templateId !== 0) {
            this.loadTemplate(this.builder.templateId);
        }
    };
    BuilderOptionComponent.prototype.loadTemplate = function (id) {
        var _this = this;
        this.client.get("/api/Template/" + id).subscribe(function (res) {
            _this.builder.template = res;
        });
    };
    BuilderOptionComponent.prototype.TemplateButton = function () {
        return this.builder.template.templateName !== "" ? this.builder.template.templateName : '选择';
    };
    BuilderOptionComponent.prototype.submitForm = function () {
        for (var i in this.validateForm.controls) {
            if (this.validateForm.controls.hasOwnProperty(i)) {
                this.validateForm.controls[i].markAsDirty();
                this.validateForm.controls[i].updateValueAndValidity();
            }
        }
        if (!this.validateForm.invalid) {
            this.builder.projectId = this.projectid;
            this.builder.fileExtensions = this.validateForm.controls['fileExtensions'].value;
            this.builder.isIgnorePrefix = this.validateForm.controls['isIgnorePrefix'].value;
            this.builder.name = this.validateForm.controls['name'].value;
            this.builder.prefix = this.validateForm.controls['prefix'].value;
            this.builder.splitDot = this.validateForm.controls['splitDot'].value;
            this.builder.outPutPath = this.validateForm.controls['outPutPath'].value;
            this.builder.mode = this.validateForm.controls['mode'].value;
            this.builder.suffix = this.validateForm.controls['suffix'].value;
            this.builder.classBase = this.validateForm.controls['classBase'].value;
            if (this.builder.id && this.builder.id !== 0) {
                this.UpdateBuilder();
                return;
            }
            this.AddBuilder();
        }
    };
    BuilderOptionComponent.prototype.ngOnInit = function () {
        this.builder.template = new src_app_template_modals_template__WEBPACK_IMPORTED_MODULE_5__["Template"]();
        this.validateForm = this.fb.group({
            name: ["" + this.builder.name, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            isServiceOnly: ["" + this.builder.isServiceOnly, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            prefix: ["" + this.builder.prefix, []],
            splitDot: ["" + this.builder.splitDot, []],
            isIgnorePrefix: ["" + this.builder.isIgnorePrefix, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            outPutPath: ["" + this.builder.outPutPath, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            mode: ["" + this.builder.mode, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            suffix: ["" + this.builder.suffix, []],
            classBase: ["" + this.builder.classBase, []],
            fileExtensions: ["" + this.builder.fileExtensions, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
        console.log(this.validateForm, 'modal');
    };
    BuilderOptionComponent.prototype.AddBuilder = function () {
        var _this = this;
        this.client.post("/api/project/Builder/New", this.builder).subscribe(function (res) {
            _this.message.success("\u6210\u529F\u4E0A\u4F20\u914D\u7F6E");
            _this.builder = res;
            console.log(res);
            _this.callBack.emit(_this.builder);
        }, function (err) {
            console.log(err);
            _this.message.error(err);
        });
    };
    BuilderOptionComponent.prototype.UpdateBuilder = function () {
        var _this = this;
        this.client.put("/api/project/Builder", this.builder).subscribe(function (res) {
            _this.message.success("\u6210\u529F\u4FEE\u6539\u914D\u7F6E");
            _this.builder = res;
            _this.callBack.emit(_this.builder);
        }, function (err) {
            _this.message.error(err);
        });
    };
    BuilderOptionComponent.prototype.selectTemplate = function () {
        this.templateSel = true;
    };
    BuilderOptionComponent.prototype.selectCallBack = function (e) {
        console.log(e);
        this.builder.templateId = e.id;
        this.builder.template = e;
        this.templateSel = false;
    };
    BuilderOptionComponent.ctorParameters = function () { return [
        { type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"] },
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_4__["NzMessageService"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], BuilderOptionComponent.prototype, "projectid", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", _modals_project__WEBPACK_IMPORTED_MODULE_2__["BuilderOptions"])
    ], BuilderOptionComponent.prototype, "builder", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], BuilderOptionComponent.prototype, "callBack", void 0);
    BuilderOptionComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-builder-option',
            template: __importDefault(__webpack_require__(/*! raw-loader!./builderOption.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/builderOption/builderOption.component.html")).default,
            styles: [""]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_4__["NzMessageService"]])
    ], BuilderOptionComponent);
    return BuilderOptionComponent;
}());



/***/ }),

/***/ "./src/app/project/newproject/buildtask.component.ts":
/*!***********************************************************!*\
  !*** ./src/app/project/newproject/buildtask.component.ts ***!
  \***********************************************************/
/*! exports provided: BuildTaskComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BuildTaskComponent", function() { return BuildTaskComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};




var BuildTaskComponent = /** @class */ (function () {
    function BuildTaskComponent(client, message, activateInfo) {
        this.client = client;
        this.message = message;
        this.activateInfo = activateInfo;
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.loading = true;
    }
    BuildTaskComponent.prototype.ngOnChanges = function (changes) {
        this.projectid = changes['projectid']['currentValue'];
        if (this.projectid && this.projectid !== 0) {
            this.run(this.projectid);
        }
    };
    BuildTaskComponent.prototype.ngOnInit = function () {
    };
    BuildTaskComponent.prototype.run = function (id) {
        var _this = this;
        this.client.post("/api/project/task/build/" + this.projectid, null)
            .subscribe(function (res) {
            _this.loading = false;
            _this.message.success("\u6210\u529F\u751F\u6210");
            _this.callBack.emit(true);
        }, function (err) {
            _this.message.error(err);
            _this.loading = false;
            _this.callBack.emit(false);
        });
    };
    BuildTaskComponent.ctorParameters = function () { return [
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__["NzMessageService"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], BuildTaskComponent.prototype, "projectid", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], BuildTaskComponent.prototype, "callBack", void 0);
    BuildTaskComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-build-task',
            template: "\n  <nz-spin *ngIf=\"this.loading\" nzTip=\"\u6B63\u5728\u751F\u6210\u4EE3\u7801\u4E2D\">\n      <nz-alert\n        nzType=\"info\"\n        nzMessage=\"\u540E\u53F0\u6B63\u5728\u751F\u6210\u4EE3\u7801\u4E2D\"\n        nzDescription=\"\u6B63\u5728\u4E3A\u60A8\u751F\u6210\u4EE3\u7801\"\n      >\n      </nz-alert>\n    </nz-spin>\n  ",
            styles: [""]
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__["NzMessageService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"]])
    ], BuildTaskComponent);
    return BuildTaskComponent;
}());



/***/ }),

/***/ "./src/app/project/newproject/generatormode.component.ts":
/*!***************************************************************!*\
  !*** ./src/app/project/newproject/generatormode.component.ts ***!
  \***************************************************************/
/*! exports provided: GeneratorModeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorModeComponent", function() { return GeneratorModeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _modals_generatormodeconfig__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../modals/generatormodeconfig */ "./src/app/project/modals/generatormodeconfig.ts");
/* harmony import */ var _modals_project__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../modals/project */ "./src/app/project/modals/project.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};







var GeneratorModeComponent = /** @class */ (function () {
    function GeneratorModeComponent(fb, modalService, message, router, client) {
        this.fb = fb;
        this.modalService = modalService;
        this.message = message;
        this.router = router;
        this.client = client;
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.project = new _modals_project__WEBPACK_IMPORTED_MODULE_6__["Project"]();
        this.dataSourceType = ['MySql', 'SqlServer', 'PostgreSQL', 'Oracle',
            'Sqlite', 'OdbcOracle', 'OdbcSqlServer', 'OdbcMySql',
            'OdbcPostgreSQL', 'Odbc', 'OdbcDameng', 'MsAccess'];
        this.generatorModeConfig = new _modals_generatormodeconfig__WEBPACK_IMPORTED_MODULE_5__["GeneratorModeConfig"](0);
    }
    GeneratorModeComponent.prototype.ngOnChanges = function (changes) {
        this.project = changes['project']['currentValue'];
        console.log(this.project, "projectOnChange");
        if (this.project.generatorModeConfig) {
            this.generatorModeConfig = this.project.generatorModeConfig;
            console.log(this.project, 'initValidateForm');
            this.initValidateForm();
            this.baseEntity();
        }
    };
    GeneratorModeComponent.prototype.submitForm = function () {
        this.generatorModeConfig.projectId = this.project.id;
        this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
        this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
        this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
        if (!this.generatorModeConfig.generatorMode) {
            this.message.warning("\u672A\u68C0\u6D4B\u5230\u6A21\u5F0F\u65E0\u6CD5\u63D0\u4EA4");
            return;
        }
        if (!this.generatorModeConfig.projectId) {
            this.message.warning("\u672A\u68C0\u6D4B\u5230\u76F8\u5173\u9879\u76EE \u8BF7\u5148\u63D0\u4EA4\u9879\u76EE\u8BE6\u60C5");
            return;
        }
        if (this.generatorModeConfig.entityAssemblyName === '') {
            this.message.warning("\u5FC5\u987B\u9009\u62E9\u4E00\u4E2A\u7A0B\u5E8F\u96C6\u8FDB\u884C\u53CD\u5C04");
            return;
        }
        if (this.generatorModeConfig.id && this.generatorModeConfig.id !== 0) {
            this.updateConfig();
            return;
        }
        this.newConfig();
    };
    GeneratorModeComponent.prototype.newConfig = function () {
        var _this = this;
        this.client.post("/api/project/Config/New", this.generatorModeConfig).subscribe(function (res) {
            _this.project.generatorModeConfig = res;
            _this.callBack.emit(_this.project);
            _this.message.success("\u6210\u529F\u63D0\u4EA4,\u53EF\u4EE5\u6267\u884C\u4E0B\u4E00\u6B65");
        });
    };
    GeneratorModeComponent.prototype.updateConfig = function () {
        var _this = this;
        this.client.put("/api/project/Config", this.generatorModeConfig).subscribe(function (res) {
            _this.project.generatorModeConfig = res;
            _this.callBack.emit(_this.project);
            _this.message.success("\u6210\u529F\u66F4\u65B0,\u53EF\u4EE5\u6267\u884C\u4E0B\u4E00\u6B65");
        });
    };
    GeneratorModeComponent.prototype.ngOnInit = function () {
        this.getAssemblies();
        if (this.project.generatorModeConfig) {
            this.generatorModeConfig = this.project.generatorModeConfig;
        }
        else {
            this.generatorModeConfig = new _modals_generatormodeconfig__WEBPACK_IMPORTED_MODULE_5__["GeneratorModeConfig"](this.project.id);
        }
        this.initValidateForm();
    };
    GeneratorModeComponent.prototype.initValidateForm = function () {
        this.validateForm = this.fb.group({
            generatorMode: ["" + this.generatorModeConfig.generatorMode, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            entityBaseName: ["" + this.generatorModeConfig.entityBaseName, []],
            entityAssemblyName: ["" + this.generatorModeConfig.entityAssemblyName, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
    };
    GeneratorModeComponent.prototype.getAssemblies = function () {
        var _this = this;
        this.client.get("/api/Assemblies").subscribe(function (res) {
            _this.assemblyList = res;
        });
    };
    GeneratorModeComponent.prototype.assemblyChange = function (e) {
        console.log(e);
        this.baseEntity();
        console.log('assemblyChange');
    };
    GeneratorModeComponent.prototype.entityBaseChange = function (e) {
        this.preview();
        console.log('entityBaseChange');
    };
    GeneratorModeComponent.prototype.baseEntity = function () {
        var _this = this;
        // this.validateForm.setValue({ 'entityBaseName': [''] });
        this.client.get("/api/BaseClass/" + this.validateForm.controls['entityAssemblyName'].value).subscribe(function (data) {
            _this.itemList = data;
        });
        this.validateForm.patchValue({ 'entityBaseName': '' });
        this.preview();
    };
    GeneratorModeComponent.prototype.preview = function () {
        this.validateForm.controls['entityAssemblyName'].markAsDirty();
        this.validateForm.controls['entityAssemblyName'].updateValueAndValidity();
        if (!this.validateForm.invalid) {
            this.generatorModeConfig.entityAssemblyName = this.validateForm.controls['entityAssemblyName'].value;
            this.generatorModeConfig.generatorMode = this.validateForm.controls['generatorMode'].value;
            this.generatorModeConfig.entityBaseName = this.validateForm.controls['entityBaseName'].value;
            this.previewShow = true;
        }
        else {
            this.previewShow = false;
        }
    };
    GeneratorModeComponent.prototype.getAllTable = function () {
        if (this.project) {
            this.generatorModeConfig.projectId = this.project.id;
        }
        else {
            this.message.warning("\u65E0\u6CD5\u627E\u5230\u9879\u76EE");
        }
    };
    GeneratorModeComponent.prototype.getIgnoreTables = function (e) {
        this.generatorModeConfig.ignoreTables = e;
    };
    GeneratorModeComponent.ctorParameters = function () { return [
        { type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"] },
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClient"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], GeneratorModeComponent.prototype, "callBack", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", _modals_project__WEBPACK_IMPORTED_MODULE_6__["Project"])
    ], GeneratorModeComponent.prototype, "project", void 0);
    GeneratorModeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-generator-mode',
            template: "\n    <form nz-form [formGroup]=\"validateForm\" >\n    <nz-form-item>\n        <nz-form-label [nzSm]=\"4\" [nzXs]=\"24\" nzFor=\"generatorMode\" nzRequired>\n          <span>\u751F\u6210\u5668\u6A21\u5F0F\n            <i nz-icon nz-tooltip nzTitle=\"CodeFirst\u4ECE\u4EE3\u7801\u5B9E\u4F53\u6D3E\u751F\u6240\u6709\u903B\u8F91DbFirst\u4ECE\u6570\u636E\u5E93\u7ED3\u6784\u6D3E\u751F\" nzType=\"question-circle\"\n              nzTheme=\"outline\"></i>\n          </span>\n        </nz-form-label>\n        <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" nzErrorTip=\"CodeFirst/DbFirst\">\n          <nz-radio-group formControlName=\"generatorMode\">\n            <label nz-radio [nzDisabled]=\"true\" nzValue=0>DbFirst</label>\n            <label nz-radio nzValue=1>CodeFirst(\u5F53\u524DWeb\u9879\u76EE)</label>\n          </nz-radio-group>\n        </nz-form-control>\n      </nz-form-item>\n      <nz-card  nzTitle=\"\u6A21\u5F0F\u76F8\u5173\u914D\u7F6E\u9879\">\n        <div id=\"dbSetting\" *ngIf=\"this.validateForm.get('generatorMode').value === '0'\">\n          <div nz-col [nzSpan]=\"10\">\n            <nz-form-item>\n              <nz-form-label nz-col [nzSm]=\"6\" [nzXs]=\"24\">\u6570\u636E\u5E93\u7C7B\u578B</nz-form-label>\n              <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\">\n                <nz-select formControlName=\"dataSourceType\">\n                  <nz-option *ngFor=\"let p of this.dataSourceType\" [nzValue]=\"p\" [nzLabel]=\"p\">\n                  </nz-option>\n                </nz-select>\n              </nz-form-control>\n            </nz-form-item>\n          </div>\n          <div nz-col [nzSpan]=\"14\">\n            <nz-form-item>\n              <nz-form-label nz-col [nzSm]=\"4\" [nzXs]=\"24\">\u6570\u636E\u5E93\u540D</nz-form-label>\n              <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\">\n                <input nz-input type=\"text\" formControlName=\"dataSourceDb\" id=\"dataSourceDb\" name='dataSourceDb'\n                  placeholder=\"\u6570\u636E\u5E93\u540D\u79F0\" />\n              </nz-form-control>\n            </nz-form-item>\n          </div>\n          <div nz-col [nzSpan]=\"10\">\n            <nz-form-item>\n              <nz-form-label nz-col [nzSm]=\"8\" [nzXs]=\"24\">\u6570\u636E\u5E93\u8FDE\u63A5\u5B57\u7B26\u4E32</nz-form-label>\n              <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\">\n                <input nz-input type=\"text\" formControlName=\"dataSourceConnectionStr\" id=\"dataSourceConnectionStr\"\n                  name='dataSourceConnectionStr' placeholder=\"\u6570\u636E\u5E93\u8FDE\u63A5\u5B57\u7B26\u4E32\" />\n              </nz-form-control>\n            </nz-form-item>\n          </div>\n        </div>\n        <div id=\"baseEntity\" *ngIf=\"this.validateForm.get('generatorMode').value === '1'\">\n          <div nz-col [nzSpan]=\"14\">\n            <nz-form-item>\n              <nz-form-label nz-col [nzSm]=\"4\" [nzXs]=\"24\">\u9009\u62E9\u76F8\u5173\u7A0B\u5E8F\u96C6/\u5B9E\u4F53jilei </nz-form-label>\n              <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" >\n                    <nz-select formControlName=\"entityAssemblyName\" nzAllowClear nzPlaceHolder=\"\u9009\u62E9\u7A0B\u5E8F\u96C6\" (ngModelChange)=\"assemblyChange($event)\">\n                  <nz-option *ngFor=\"let item of assemblyList\" [nzLabel]=\"item.key\" [nzValue]=\"item.value\" ></nz-option>\n                </nz-select>\n              </nz-form-control>\n              <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" >\n                    <nz-select formControlName=\"entityBaseName\" nzAllowClear nzPlaceHolder=\"\u9009\u62E9\u57FA\u7C7B\"  (ngModelChange)=\"entityBaseChange($event)\" >\n                  <nz-option *ngFor=\"let item of itemList\" [nzLabel]=\"item.key\" [nzValue]=\"item.value\"></nz-option>\n                </nz-select>\n              </nz-form-control>\n            </nz-form-item>\n          </div>\n          <div nz-col [nzSpan]=\"8\">\n            <nz-form-control  [nzSm]=\"5\" [nzXs]=\"24\">\n              <button nz-button style=\"width:100%\" (click)='submitForm()' [nzType]=\"'primary'\">\u63D0\u4EA4</button>\n            </nz-form-control>\n          </div>\n        </div>\n      </nz-card>\n      <nz-form-item>\n      <div nz-col [nzSpan]=\"24\" *ngIf=\"this.previewShow\">\n        <app-table-preview  (allTable)=\"getAllTable($event)\"\n         [ignoreTables] = \"this.generatorModeConfig.ignoreTables\"\n         (callBack) = \"getIgnoreTables($event)\"\n         [entityAssemblyName]=\"this.validateForm.get('entityAssemblyName').value\"\n         [entityBaseName]=\"this.validateForm.get('entityBaseName').value\">\n        </app-table-preview>\n      </div>\n      </nz-form-item>\n</form>\n    "
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"], _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClient"]])
    ], GeneratorModeComponent);
    return GeneratorModeComponent;
}());



/***/ }),

/***/ "./src/app/project/newproject/newproject.component.ts":
/*!************************************************************!*\
  !*** ./src/app/project/newproject/newproject.component.ts ***!
  \************************************************************/
/*! exports provided: NewProjectComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NewProjectComponent", function() { return NewProjectComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _modals_project__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../modals/project */ "./src/app/project/modals/project.ts");
/* harmony import */ var ng_zorro_antd_modal__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ng-zorro-antd/modal */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd-modal.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};







var NewProjectComponent = /** @class */ (function () {
    function NewProjectComponent(fb, modalService, message, activateInfo, router, client) {
        var _this = this;
        this.fb = fb;
        this.modalService = modalService;
        this.message = message;
        this.activateInfo = activateInfo;
        this.router = router;
        this.client = client;
        this.current = 0;
        this.isVisible = new Map().set('builder', false).set('globalBuilder', false);
        this.builderVisable = false;
        this.gBuilderVisable = false;
        this.csprojPath = '';
        this.previewShow = false;
        this.currentFinish = new Map();
        for (var index = 0; index < 6; index++) {
            this.currentFinish.set(index, false);
        }
        this.project = new _modals_project__WEBPACK_IMPORTED_MODULE_2__["Project"]();
        this.activateInfo.queryParams.subscribe(function (queryParams) {
            _this.projectId = queryParams.id;
            if (_this.projectId && _this.projectId !== 0) {
                // this.loadProject(this.projectId);
            }
        });
    }
    NewProjectComponent.prototype.callBack = function (e) {
        this.project = e;
        console.log(this.project, "projectCallback");
        if (this.project.projectInfoId !== 0) {
            this.currentFinish[0] = true;
        }
        if (this.project.generatorModeConfigId !== 0) {
            this.currentFinish[1] = true;
        }
        if (this.project.builders.length > 0) {
            this.currentFinish[2] = true;
        }
        this.currentFinish[3] = true;
        console.log(this.currentFinish, 'onload');
        this.projectId = e.id;
        this.currentFinish[this.current] = true;
    };
    NewProjectComponent.prototype.builderCallBack = function (e) {
        console.log(e.map(function (x) { return x.id; }));
        this.project.builders = e;
        if (this.project.builders.length > 0) {
            this.currentFinish[2] = true;
            this.currentFinish[3] = true;
        }
    };
    NewProjectComponent.prototype.IsFinish = function (e) {
        if (e) {
            this.current += 1;
        }
    };
    NewProjectComponent.prototype.pre = function () {
        this.current -= 1;
    };
    NewProjectComponent.prototype.next = function () {
        this.current += 1;
    };
    NewProjectComponent.prototype.ngOnInit = function () {
        this.project.builders = new Array();
        this.project.globalBuilders = new Array();
    };
    NewProjectComponent.prototype.finished = function () {
        this.router.navigateByUrl("project/home");
    };
    NewProjectComponent.prototype.getCodePath = function () {
        return this.project.projectInfo.rootPath + "\\" + this.project.projectInfo.outPutPath + "\\";
    };
    NewProjectComponent.prototype.back = function (e) {
        this.router.navigateByUrl("project/home");
    };
    NewProjectComponent.ctorParameters = function () { return [
        { type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"] },
        { type: ng_zorro_antd_modal__WEBPACK_IMPORTED_MODULE_3__["NzModalService"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_6__["NzMessageService"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_4__["ActivatedRoute"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"] },
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClient"] }
    ]; };
    NewProjectComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-newproject',
            template: __importDefault(__webpack_require__(/*! raw-loader!./newproject.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/newproject/newproject.component.html")).default,
            styles: ["\n\n  .stepbtn{\n    margin:10px;\n  }\n\n  .steps-content {\n        margin-top: 16px;\n        border: 1px dashed #e9e9e9;\n        border-radius: 6px;\n        background-color: #fafafa;\n        min-height: 500px;\n        text-align: left;\n        padding-top: 80px;\n      }\n  "]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], ng_zorro_antd_modal__WEBPACK_IMPORTED_MODULE_3__["NzModalService"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_6__["NzMessageService"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"], _angular_common_http__WEBPACK_IMPORTED_MODULE_5__["HttpClient"]])
    ], NewProjectComponent);
    return NewProjectComponent;
}());



/***/ }),

/***/ "./src/app/project/newproject/projectinfo.component.ts":
/*!*************************************************************!*\
  !*** ./src/app/project/newproject/projectinfo.component.ts ***!
  \*************************************************************/
/*! exports provided: ProjectinfoComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectinfoComponent", function() { return ProjectinfoComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _modals_projectInfo__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../modals/projectInfo */ "./src/app/project/modals/projectInfo.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};






var ProjectinfoComponent = /** @class */ (function () {
    function ProjectinfoComponent(fb, modalService, message, activateInfo, router, client) {
        var _this = this;
        this.fb = fb;
        this.modalService = modalService;
        this.message = message;
        this.activateInfo = activateInfo;
        this.router = router;
        this.client = client;
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.projectInfo = new _modals_projectInfo__WEBPACK_IMPORTED_MODULE_5__["ProjectInfo"]();
        this.isEdite = false;
        this.activateInfo.queryParams.subscribe(function (queryParams) {
            _this.projectId = queryParams.id;
            if (_this.projectId !== 0) {
                _this.isEdite = true;
            }
        });
    }
    ProjectinfoComponent.prototype.submitForm = function () {
        for (var key in this.validateForm.controls) {
            if (this.validateForm.controls.hasOwnProperty(key)) {
                this.validateForm.controls[key].markAsDirty();
                this.validateForm.controls[key].updateValueAndValidity();
            }
        }
        this.projectInfo.outPutPath = this.validateForm.controls['outPutPath'].value;
        this.projectInfo.author = this.validateForm.controls['author'].value;
        this.projectInfo.projectName = this.validateForm.controls['projectName'].value;
        this.projectInfo.rootPath = this.validateForm.controls['rootPath'].value;
        if (!this.validateForm.invalid) {
            this.submit();
        }
    };
    ProjectinfoComponent.prototype.ngOnInit = function () {
        if (this.projectId) {
            this.loadProject(this.projectId);
        }
        this.controlerLoad();
    };
    ProjectinfoComponent.prototype.controlerLoad = function () {
        this.validateForm = this.fb.group({
            projectName: [this.projectInfo.projectName, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(255)]],
            rootPath: [this.projectInfo.rootPath, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(255)]],
            outPutPath: [this.projectInfo.outPutPath, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(255)]],
            author: [this.projectInfo.author, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(255)]]
        });
    };
    ProjectinfoComponent.prototype.loadProject = function (id) {
        var _this = this;
        this.client.get("/api/project/" + id).subscribe(function (res) {
            _this.projectInfo = res.projectInfo;
            _this.callBack.emit(res);
            _this.controlerLoad();
        });
    };
    ProjectinfoComponent.prototype.submit = function () {
        var _this = this;
        console.log(this.projectInfo, 'info');
        if (this.projectInfo.id && this.projectInfo.id !== 0) {
            console.log("put");
            this.client.put("/api/project/Info", this.projectInfo)
                .subscribe(function (res) {
                _this.message.success('修改成功');
                _this.callBack.emit(res);
            });
            return;
        }
        this.client.post("/api/project/Info/New", this.projectInfo)
            .subscribe(function (res) {
            _this.message.success('添加成功 可以进行下一步');
            _this.callBack.emit(res);
            console.log(_this.callBack);
        });
    };
    ProjectinfoComponent.ctorParameters = function () { return [
        { type: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"] },
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClient"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], ProjectinfoComponent.prototype, "callBack", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], ProjectinfoComponent.prototype, "projectId", void 0);
    ProjectinfoComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-project-info',
            template: "\n<form nz-form [formGroup]=\"validateForm\" (ngSubmit)=\"submitForm()\">\n      <nz-form-item>\n        <nz-form-label [nzSm]=\"6\" [nzXs]=\"24\" nzRequired nzFor=\"projectName\">\u9879\u76EE\u540D\u79F0</nz-form-label>\n        <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" nzErrorTip=\"\u8FD9\u91CC\u5199\u4E0B\u9879\u76EE\u540D\u79F0\">\n          <input nz-input formControlName=\"projectName\" id=\"projectName\" placeholder=\"\u9879\u76EE\u540D\u79F0\" />\n        </nz-form-control>\n      </nz-form-item>\n      <nz-form-item>\n        <nz-form-label [nzSm]=\"6\" [nzXs]=\"24\" nzFor=\"rootPath\" nzRequired>\u9879\u76EE\u6839\u76EE\u5F55\u8DEF\u5F84</nz-form-label>\n        <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" nzErrorTip=\"\u6839\u76EE\u5F55\u8DEF\u5F84\u5FC5\u987B\u586B\u5199\">\n          <input nz-input type=\"text\" id=\"rootPath\" formControlName=\"rootPath\" placeholder=\"\u9879\u76EE\u6587\u4EF6\u6700\u7EC8\u8F93\u51FA\u8DEF\u5F84=\u9879\u76EE\u6839\u76EE\u5F55\u8DEF\u5F84\u8F93\u51FA\u8DEF\u5F84\" />\n        </nz-form-control>\n      </nz-form-item>\n      <nz-form-item>\n        <nz-form-label [nzSm]=\"6\" [nzXs]=\"24\" nzFor=\"outPutPath\" nzRequired>\u8F93\u51FA\u8DEF\u5F84</nz-form-label>\n        <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\" nzErrorTip=\"\u8F93\u51FA\u8DEF\u5F84\u5FC5\u987B\u586B\u5199\" >\n          <input nz-input type=\"text\" formControlName=\"outPutPath\" id=\"outPutPath\" placeholder=\"\u9879\u76EE\u6587\u4EF6\u6700\u7EC8\u8F93\u51FA\u8DEF\u5F84=\u9879\u76EE\u6839\u76EE\u5F55\u8DEF\u5F84\u8F93\u51FA\u8DEF\u5F84\" />\n        </nz-form-control>\n      </nz-form-item>\n      <nz-form-item>\n        <nz-form-label [nzSm]=\"6\" [nzXs]=\"24\" nzFor=\"author\" nzRequired>\u4F5C\u8005</nz-form-label>\n        <nz-form-control [nzSm]=\"10\" [nzXs]=\"24\"  nzErrorTip=\"\u4F5C\u8005\u5FC5\u987B\u586B\u5199\">\n          <input nz-input type=\"text\" formControlName=\"author\" id=\"author\" placeholder=\"\u4F5C\u8005\u4F1A\u63D0\u73B0\u5728\u4EE3\u7801\u751F\u6210\u5668\u7684\u5907\u6CE8\u4E2D\" />\n        </nz-form-control>\n      </nz-form-item>\n      <nz-form-item>\n        <nz-form-control  [nzOffset]=\"6\" [nzXs]=\"18\"   >\n          <button nz-button type='submit'   [nzType]=\"'primary'\" >\u63D0\u4EA4</button>\n        </nz-form-control>\n      </nz-form-item>\n</form>\n  ",
            styles: [""]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzModalService"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_2__["NzMessageService"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"], _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClient"]])
    ], ProjectinfoComponent);
    return ProjectinfoComponent;
}());



/***/ }),

/***/ "./src/app/project/project.component.css":
/*!***********************************************!*\
  !*** ./src/app/project/project.component.css ***!
  \***********************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3Byb2plY3QvcHJvamVjdC5jb21wb25lbnQuY3NzIn0= */");

/***/ }),

/***/ "./src/app/project/project.component.ts":
/*!**********************************************!*\
  !*** ./src/app/project/project.component.ts ***!
  \**********************************************/
/*! exports provided: ProjectComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectComponent", function() { return ProjectComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};


var ProjectComponent = /** @class */ (function () {
    function ProjectComponent(router) {
        this.router = router;
    }
    ProjectComponent.prototype.newProject = function () {
        this.router.navigateByUrl('/project/new');
    };
    ProjectComponent.prototype.projectList = function () {
        this.router.navigateByUrl('/project/list');
    };
    ProjectComponent.prototype.ngOnInit = function () {
    };
    ProjectComponent.ctorParameters = function () { return [
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"] }
    ]; };
    ProjectComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-project',
            template: __importDefault(__webpack_require__(/*! raw-loader!./project.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/project.component.html")).default,
            styles: [__importDefault(__webpack_require__(/*! ./project.component.css */ "./src/app/project/project.component.css")).default]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], ProjectComponent);
    return ProjectComponent;
}());



/***/ }),

/***/ "./src/app/project/project.module.ts":
/*!*******************************************!*\
  !*** ./src/app/project/project.module.ts ***!
  \*******************************************/
/*! exports provided: ProjectModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectModule", function() { return ProjectModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _project_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./project.component */ "./src/app/project/project.component.ts");
/* harmony import */ var _newproject_newproject_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./newproject/newproject.component */ "./src/app/project/newproject/newproject.component.ts");
/* harmony import */ var _newproject_builderOption_builderOption_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./newproject/builderOption/builderOption.component */ "./src/app/project/newproject/builderOption/builderOption.component.ts");
/* harmony import */ var _project_routing_module__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./project.routing.module */ "./src/app/project/project.routing.module.ts");
/* harmony import */ var _projectList_projectList_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./projectList/projectList.component */ "./src/app/project/projectList/projectList.component.ts");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ng_zorro_antd_form__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ng-zorro-antd/form */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd-form.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _tablePreview_tablePreview_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./tablePreview/tablePreview.component */ "./src/app/project/tablePreview/tablePreview.component.ts");
/* harmony import */ var _template_template_module__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../template/template.module */ "./src/app/template/template.module.ts");
/* harmony import */ var _newproject_projectinfo_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./newproject/projectinfo.component */ "./src/app/project/newproject/projectinfo.component.ts");
/* harmony import */ var _newproject_generatormode_component__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./newproject/generatormode.component */ "./src/app/project/newproject/generatormode.component.ts");
/* harmony import */ var _newproject_builderOption_BuilderContainer_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./newproject/builderOption/BuilderContainer.component */ "./src/app/project/newproject/builderOption/BuilderContainer.component.ts");
/* harmony import */ var _newproject_buildtask_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./newproject/buildtask.component */ "./src/app/project/newproject/buildtask.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};


















// import { Http } from '../base/webapi';
var ProjectModule = /** @class */ (function () {
    function ProjectModule() {
    }
    ProjectModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            exports: [_project_component__WEBPACK_IMPORTED_MODULE_2__["ProjectComponent"], _newproject_newproject_component__WEBPACK_IMPORTED_MODULE_3__["NewProjectComponent"], _newproject_builderOption_builderOption_component__WEBPACK_IMPORTED_MODULE_4__["BuilderOptionComponent"], _newproject_projectinfo_component__WEBPACK_IMPORTED_MODULE_13__["ProjectinfoComponent"],
                _newproject_generatormode_component__WEBPACK_IMPORTED_MODULE_14__["GeneratorModeComponent"], _newproject_builderOption_BuilderContainer_component__WEBPACK_IMPORTED_MODULE_15__["BuilderContainerComponent"], _newproject_buildtask_component__WEBPACK_IMPORTED_MODULE_16__["BuildTaskComponent"]],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _project_routing_module__WEBPACK_IMPORTED_MODULE_5__["ProjectRoutingModule"],
                ng_zorro_antd__WEBPACK_IMPORTED_MODULE_7__["NgZorroAntdModule"],
                ng_zorro_antd_form__WEBPACK_IMPORTED_MODULE_9__["NzFormModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_8__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_8__["ReactiveFormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_10__["HttpClientModule"],
                _template_template_module__WEBPACK_IMPORTED_MODULE_12__["TemplateModule"]
            ],
            declarations: [_project_component__WEBPACK_IMPORTED_MODULE_2__["ProjectComponent"], _newproject_newproject_component__WEBPACK_IMPORTED_MODULE_3__["NewProjectComponent"], _tablePreview_tablePreview_component__WEBPACK_IMPORTED_MODULE_11__["TablePreViewComponent"], _newproject_projectinfo_component__WEBPACK_IMPORTED_MODULE_13__["ProjectinfoComponent"], _newproject_generatormode_component__WEBPACK_IMPORTED_MODULE_14__["GeneratorModeComponent"],
                _newproject_builderOption_BuilderContainer_component__WEBPACK_IMPORTED_MODULE_15__["BuilderContainerComponent"], _projectList_projectList_component__WEBPACK_IMPORTED_MODULE_6__["ProjectListComponent"], _newproject_builderOption_builderOption_component__WEBPACK_IMPORTED_MODULE_4__["BuilderOptionComponent"], _newproject_buildtask_component__WEBPACK_IMPORTED_MODULE_16__["BuildTaskComponent"]]
        })
    ], ProjectModule);
    return ProjectModule;
}());



/***/ }),

/***/ "./src/app/project/project.routing.module.ts":
/*!***************************************************!*\
  !*** ./src/app/project/project.routing.module.ts ***!
  \***************************************************/
/*! exports provided: ProjectRoutes, ProjectRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectRoutes", function() { return ProjectRoutes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectRoutingModule", function() { return ProjectRoutingModule; });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _project_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./project.component */ "./src/app/project/project.component.ts");
/* harmony import */ var _newproject_newproject_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./newproject/newproject.component */ "./src/app/project/newproject/newproject.component.ts");
/* harmony import */ var _projectList_projectList_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./projectList/projectList.component */ "./src/app/project/projectList/projectList.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};





var routes = [
    {
        path: 'home', component: _project_component__WEBPACK_IMPORTED_MODULE_2__["ProjectComponent"],
        data: {
            breadcrumb: '项目主页'
        }
    },
    {
        path: 'new', component: _newproject_newproject_component__WEBPACK_IMPORTED_MODULE_3__["NewProjectComponent"],
        data: {
            breadcrumb: '新增项目'
        }
    },
    {
        path: 'list', component: _projectList_projectList_component__WEBPACK_IMPORTED_MODULE_4__["ProjectListComponent"],
        data: {
            breadcrumb: '项目列表'
        }
    }
];
var ProjectRoutes = _angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"].forChild(routes);
var ProjectRoutingModule = /** @class */ (function () {
    function ProjectRoutingModule() {
    }
    ProjectRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            imports: [
                ProjectRoutes
            ]
        })
    ], ProjectRoutingModule);
    return ProjectRoutingModule;
}());



/***/ }),

/***/ "./src/app/project/projectList/projectList.component.css":
/*!***************************************************************!*\
  !*** ./src/app/project/projectList/projectList.component.css ***!
  \***************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3Byb2plY3QvcHJvamVjdExpc3QvcHJvamVjdExpc3QuY29tcG9uZW50LmNzcyJ9 */");

/***/ }),

/***/ "./src/app/project/projectList/projectList.component.ts":
/*!**************************************************************!*\
  !*** ./src/app/project/projectList/projectList.component.ts ***!
  \**************************************************************/
/*! exports provided: ProjectListComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectListComponent", function() { return ProjectListComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_base_modalbase__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/base/modalbase */ "./src/app/base/modalbase.ts");
/* harmony import */ var ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ng-zorro-antd */ "./node_modules/ng-zorro-antd/fesm5/ng-zorro-antd.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};





var ProjectListComponent = /** @class */ (function () {
    function ProjectListComponent(client, router, message) {
        this.client = client;
        this.router = router;
        this.message = message;
        this.loading = false;
        this.pageData = new src_app_base_modalbase__WEBPACK_IMPORTED_MODULE_2__["PageView"]();
    }
    ProjectListComponent.prototype.loadData = function () {
        var _this = this;
        this.loading = true;
        this.client.get("/api/project/page?PageSize=" + this.pageData.pageSize + "&PageNumber=" + this.pageData.pageNumber)
            .subscribe(function (data) {
            _this.pageData = data;
            _this.loading = false;
        }, function (error) {
            console.log(error);
            _this.loading = false;
        });
    };
    ProjectListComponent.prototype.ngOnInit = function () {
        this.loadData();
    };
    ProjectListComponent.prototype.IndeChange = function (e) {
        this.pageData.pageNumber = e;
        this.loadData();
    };
    ProjectListComponent.prototype.SizeChange = function (e) {
        this.pageData.pageSize = e;
        this.loadData();
    };
    ProjectListComponent.prototype.delproj = function (id) {
        var _this = this;
        this.client.delete("/api/project/" + id).subscribe(function (res) {
            if (res > 0) {
                _this.message.success("\u5220\u9664\u6210\u529F");
                _this.loadData();
            }
        });
    };
    ProjectListComponent.prototype.renderGeneratorMode = function (data) {
        if (data.generatorModeConfig) {
            return data.generatorModeConfig.generatorMode === 0 ? 'DbFirst' : 'CodeFirst';
        }
        return '';
    };
    ProjectListComponent.prototype.gotoEdit = function (id) {
        this.router.navigate(['project/new'], {
            queryParams: {
                id: id
            }
        });
    };
    ProjectListComponent.prototype.build = function (data) {
        var _this = this;
        this.client.post("/api/project/task/build/" + data.id, null).subscribe(function (res) {
            _this.message.success("\u751F\u6210\u6210\u529F---\u5730\u5740" + data.projectInfo.rootPath + "\\" + data.projectInfo.outPutPath);
        });
    };
    ProjectListComponent.ctorParameters = function () { return [
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"] },
        { type: _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"] },
        { type: ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__["NzMessageService"] }
    ]; };
    ProjectListComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-projectlist',
            template: __importDefault(__webpack_require__(/*! raw-loader!./projectList.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/projectList/projectList.component.html")).default,
            styles: [__importDefault(__webpack_require__(/*! ./projectList.component.css */ "./src/app/project/projectList/projectList.component.css")).default]
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"],
            ng_zorro_antd__WEBPACK_IMPORTED_MODULE_3__["NzMessageService"]])
    ], ProjectListComponent);
    return ProjectListComponent;
}());



/***/ }),

/***/ "./src/app/project/tablePreview/tablePreview.component.ts":
/*!****************************************************************!*\
  !*** ./src/app/project/tablePreview/tablePreview.component.ts ***!
  \****************************************************************/
/*! exports provided: TablePreViewComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TablePreViewComponent", function() { return TablePreViewComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __importDefault = (undefined && undefined.__importDefault) || function (mod) {
  return (mod && mod.__esModule) ? mod : { "default": mod };
};


var TablePreViewComponent = /** @class */ (function () {
    function TablePreViewComponent(client) {
        this.client = client;
        this.entityBaseName = '';
        this.entityAssemblyName = '';
        this.allTable = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.callBack = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.ignoreTables = '';
        this.mapOfExpandData = {};
        this.mapOfNavExpandData = {};
    }
    TablePreViewComponent.prototype.ngOnChanges = function (changes) {
        if (changes['entityAssemblyName']) {
            this.entityAssemblyName = changes['entityAssemblyName']['currentValue'];
        }
        if (changes['entityBaseName']) {
            this.entityBaseName = changes['entityBaseName']['currentValue'];
        }
        this.getTableInfos();
    };
    TablePreViewComponent.prototype.ngOnInit = function () {
        this.getTableInfos();
    };
    TablePreViewComponent.prototype.getTableInfos = function () {
        var _this = this;
        this.client
            .get("/api/AllTable/" + this.entityAssemblyName + "?entityBaseName=" + this.entityBaseName)
            .subscribe(function (data) {
            var ignores = _this.ignoreTables.split(',');
            if (ignores.length > 0 && ignores[0] !== '') {
                console.log(ignores.length, 'ignores');
                ignores.forEach(function (ignore) {
                    data.filter(function (d) { return d.csName === ignore; })[0].isIgnore = true;
                });
            }
            _this.listOfData = data;
            _this.allTable.emit(_this.listOfData);
        });
    };
    TablePreViewComponent.prototype.ignoreTable = function (row) {
        row.isIgnore = !row.isIgnore;
        this.listOfData.filter(function (x) { return x.csName === row.csName; })[0].isIgnore = row.isIgnore;
        this.ignoreTables = this.listOfData.filter(function (x) { return x.isIgnore; }).map(function (x) { return x.csName; }).join(',');
        this.callBack.emit(this.ignoreTables);
    };
    TablePreViewComponent.prototype.getPkName = function (row) {
        return row.primarys.map(function (x) { return x.csName + "-" + x.dbTypeText; }).join(',');
    };
    TablePreViewComponent.prototype.getObjectKeys = function (objcet) {
        console.log(objcet);
        return objcet;
    };
    TablePreViewComponent.ctorParameters = function () { return [
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"] }
    ]; };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], TablePreViewComponent.prototype, "entityBaseName", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], TablePreViewComponent.prototype, "entityAssemblyName", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], TablePreViewComponent.prototype, "allTable", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"])
    ], TablePreViewComponent.prototype, "callBack", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], TablePreViewComponent.prototype, "ignoreTables", void 0);
    TablePreViewComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-table-preview',
            template: __importDefault(__webpack_require__(/*! raw-loader!./tablePreview.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/project/tablePreview/tablePreview.component.html")).default,
            styles: ["\n  .lblCol{width:20vw; display:inline-block;}\n  "]
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
    ], TablePreViewComponent);
    return TablePreViewComponent;
}());



/***/ })

}]);
//# sourceMappingURL=project-project-module.js.map