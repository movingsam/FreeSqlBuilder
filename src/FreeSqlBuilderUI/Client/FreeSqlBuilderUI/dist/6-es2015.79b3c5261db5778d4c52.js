(window.webpackJsonp=window.webpackJsonp||[]).push([[6],{"fsc+":function(e,t,i){"use strict";i.r(t),i.d(t,"GeneratorModule",(function(){return ze}));var c=i("M0ag"),s=i("iA/u"),n=i("fXoL"),o=i("dEAy"),r=i("PScX"),a=i("i8ly"),l=i("/O+n"),u=i("z8Z6"),d=i("Ac7g"),h=i("PiIy"),b=i("ofXK"),m=i("qAZ0"),f=i("ok2U"),p=i("OzZK"),g=i("RwU8"),y=i("C2AL");function S(e,t){1&e&&n.Ub(0,"nz-spin",4)}function v(e,t){if(1&e){const e=n.ac();n.Zb(0,"sf",5,6),n.Zb(2,"div",7),n.Zb(3,"button",8),n.hc("click",(function(){return n.Ec(e),n.kc().close()})),n.Oc(4,"\u5173\u95ed"),n.Yb(),n.Zb(5,"button",9),n.hc("click",(function(){n.Ec(e);const t=n.Bc(1);return n.kc().save(t.value)})),n.Oc(6," \u4fdd\u5b58 "),n.Yb(),n.Yb(),n.Yb()}if(2&e){const e=n.Bc(1),t=n.kc();n.sc("schema",t.schema)("ui",t.ui)("formData",t.i),n.Db(5),n.sc("disabled",!e.valid)("nzLoading",t.http.loading)}}let k=(()=>{class e{constructor(e,t,i,c,s,n,o){this.modal=e,this.msgSrv=t,this.service=i,this.configService=c,this.templateService=s,this.http=n,this.projectService=o,this.record={},this.schema={properties:{prefix:{type:"string",title:"\u7c7b\u540d\u524d\u7f00",maxLength:10,ui:{placeholder:"\u751f\u6210\u6587\u4ef6\u540d\u53ca\u7c7b\u540d\u7684\u524d\u7f00"}},name:{type:"string",title:"\u6784\u5efa\u5668\u540d",ui:{placeholder:"\u6784\u5efa\u5668\u540d\u79f0,\u4ec5\u7528\u6765\u533a\u5206\u5404\u4e2a\u6784\u5efa\u5668,\u4e0d\u53c2\u4e0e\u6587\u4ef6\u751f\u6210\u64cd\u4f5c"}},suffix:{type:"string",title:"\u7c7b\u540d\u540e\u7f00",ui:{placeholder:"\u751f\u6210\u6587\u4ef6\u540d\u53ca\u7c7b\u540d\u7684\u540e\u7f00"}},outPutPath:{type:"string",title:"\u8f93\u51fa\u8def\u5f84",ui:{placeholder:"\u7528\u6765\u653e\u7f6e\u6b64\u6784\u5efa\u5668\u751f\u6210\u7684\u6587\u4ef6\u5939\u540d"}},mode:{type:"number",enum:[{label:"\u9ed8\u8ba4",value:0,key:0},{label:"\u9996\u5b57\u6bcd\u5927\u5199",value:1,key:1},{label:"\u5168\u5c0f\u5199",value:2,key:2}],ui:{widget:"radio"},title:"\u540d\u79f0\u8f6c\u6362\u5668"},templateId:{type:"number",title:"\u6a21\u677f\u9009\u62e9",ui:{widget:"select",asyncData:()=>this.templateService.getTemplateSelect()}},type:{type:"number",title:"\u6784\u5efa\u5668\u7c7b\u578b",enum:[{label:"\u5355\u8868\u6784\u5efa\u5668",value:0,key:0},{label:"\u5168\u8868\u6784\u5efa\u5668",value:1,key:1}],ui:{widget:"radio"}},fileExtensions:{type:"string",title:"\u6587\u4ef6\u540e\u7f00"},defaultProjectId:{type:"number",title:"\u9ed8\u8ba4\u9879\u76ee",ui:{widget:"select",asyncData:()=>this.projectService.getSelect()}}},required:["fileExtensions","name","outPutPath","templateId"]},this.ui={"*":{spanLabelFixed:100,grid:{span:12}},$no:{widget:"text"},$href:{widget:"string"},$description:{widget:"textarea",grid:{span:24}}}}ngOnInit(){this.record.id>0&&this.service.getBuilderById(this.record.id).subscribe(e=>this.i=e),this.i=new s.a}save(e){this.record.id>0?this.service.updateBuilder(e).subscribe(e=>{this.msgSrv.success("\u66f4\u65b0\u6210\u529f"),this.modal.close(!0)}):this.service.createBuilder(e).subscribe(e=>{e.id>0&&(this.msgSrv.success("\u65b0\u589e\u6210\u529f"),this.modal.close(!0))})}close(){this.modal.destroy()}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(o.b),n.Tb(r.e),n.Tb(a.a),n.Tb(l.a),n.Tb(u.a),n.Tb(d.r),n.Tb(h.a))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-builder-edit"]],decls:5,vars:3,consts:[[1,"modal-header"],[1,"modal-title"],["class","modal-spin",4,"ngIf"],["mode","edit","button","none",3,"schema","ui","formData",4,"ngIf"],[1,"modal-spin"],["mode","edit","button","none",3,"schema","ui","formData"],["sf",""],[1,"modal-footer"],["nz-button","","type","button",3,"click"],["nz-button","","type","submit","nzType","primary",3,"disabled","nzLoading","click"]],template:function(e,t){1&e&&(n.Zb(0,"div",0),n.Zb(1,"div",1),n.Oc(2),n.Yb(),n.Yb(),n.Mc(3,S,1,0,"nz-spin",2),n.Mc(4,v,7,5,"sf",3)),2&e&&(n.Db(2),n.Qc("\u7f16\u8f91 ",t.record.id," \u4fe1\u606f"),n.Db(1),n.sc("ngIf",!t.i),n.Db(1),n.sc("ngIf",t.i))},directives:[b.m,m.a,f.b,p.a,g.a,y.a],encapsulation:2}),e})();var T=i("SdXu"),w=i("JA5x"),z=i("DGaY");const I=["st"];function x(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",5),n.hc("click",(function(){return n.Ec(e),n.kc().add()})),n.Oc(1,"\u65b0\u5efa"),n.Yb()}}let D=(()=>{class e{constructor(e,t,i,c){this.service=e,this.modal=t,this.msgServ=i,this.projectService=c,this.url="api/builder",this.searchSchema={properties:{keyword:{type:"string",title:"\u5173\u952e\u5b57"}}},this.formData=[],this.TAG={0:{text:"\u5355\u8868",color:"green"},1:{text:"\u5168\u8868",color:"blue"}},this.MODE={0:{text:"\u9ed8\u8ba4",color:"default"},1:{text:"\u9996\u5b57\u5927\u5199",color:"blue"},2:{text:"\u5168\u5c0f\u5199",color:"green"}},this.columns=[{title:"\u7f16\u53f7",index:"id"},{title:"\u6784\u5efa\u5668\u540d",index:"name"},{title:"\u8f6c\u6362\u5668\u6a21\u5f0f",index:"mode",type:"tag",tag:this.MODE},{title:"\u6a21\u677f\u540d\u79f0",index:"template.templateName"},{title:"\u6587\u4ef6\u540e\u7f00",index:"fileExtensions"},{title:"\u6784\u5efa\u5668\u7c7b\u578b",type:"tag",index:"type",tag:this.TAG},{title:"\u64cd\u4f5c",buttons:[{text:"\u751f\u6210",type:"link",click:e=>{console.log(e,"click"),this.projectService.buildTempTask(e.id).subscribe(e=>{this.msgServ.success("\u751f\u6210\u6210\u529f!\u6587\u4ef6\u5730\u5740"+e)})}},{text:"\u7f16\u8f91",type:"modal",modal:{component:k,modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}},click:(e,t)=>{!0===t&&this.st.reload()}},{text:"\u5220\u9664",type:"del",click:e=>{this.service.deleteBuilder(e.id).subscribe(t=>{this.msgServ.success("\u6210\u529f\u5220\u9664\u6784\u5efa\u5668"+e.name),this.st.reload()})}}]}]}ngOnInit(){}add(){this.modal.createStatic(k,{i:{id:0}}).subscribe(()=>this.st.reload())}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(a.a),n.Tb(d.j),n.Tb(r.e),n.Tb(h.a))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-builder"]],viewQuery:function(e,t){var i;1&e&&n.Tc(I,!0),2&e&&n.Ac(i=n.ic())&&(t.st=i.first)},decls:7,vars:4,consts:[[3,"action"],["phActionTpl",""],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["nz-button","","nzType","primary",3,"click"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"page-header",0),n.Mc(1,x,2,0,"ng-template",null,1,n.Nc),n.Yb(),n.Zb(3,"nz-card"),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb()}if(2&e){const e=n.Bc(2);n.sc("action",e),n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns)}},directives:[T.a,w.a,f.b,z.a,p.a,g.a,y.a],encapsulation:2}),e})();i("0lRi");var O=i("GA6O");const C=["ds"],E=["st"];function Z(e,t){if(1&e){const e=n.ac();n.Zb(0,"fb-datasource",6),n.hc("dataSourceChange",(function(t){return n.Ec(e),n.kc().dataSource=t}))("dataSourceChange",(function(t){return n.Ec(e),n.kc().dataSourceChange(t)})),n.Yb()}if(2&e){const e=n.kc();n.sc("dataSource",e.dataSource)}}let Y=(()=>{class e{constructor(e,t,i,c){this.config=e,this.modal=t,this.modalHelpr=i,this.msgSer=c,this.url="api/config/DataSource",this.dataSource=new s.c,this.searchSchema={properties:{keyword:{type:"string",title:"\u5173\u952e\u5b57"}}},this.columns=[{title:"\u7f16\u53f7",index:"id"},{title:"\u6570\u636e\u6e90\u540d\u79f0",index:"name"},{title:"\u6570\u636e\u5e93\u7c7b\u578b",type:"enum",enum:{0:"MySql",1:"SqlServer",2:"PostgreSQL",3:"Oracle",4:"Sqlite",5:"OdbcOracle",6:"OdbcSqlServer",7:"OdbcMySql",8:"OdbcPostgreSQL",9:"Odbc",10:"OdbcDameng",11:"MsAccess",12:"Dameng",13:"OdbcKingbaseES",14:"ShenTong"},index:"dbType"},{title:"\u6570\u636e\u5e93\u8fde\u63a5",index:"connectionString"},{title:"\u64cd\u4f5c",buttons:[{text:"\u7f16\u8f91",type:"modal",modal:{component:O.a,modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"},nzFooter:[{label:"\u786e\u5b9a",onClick:e=>{console.log(e.sf.value,"update"),this.config.updateDataSource(e.sf.value).subscribe(e=>{this.msgSer.success("\u66f4\u65b0\u6210\u529f"),this.st.reload(),this.modalHelpr.closeAll()})}}]}},click:(e,t)=>{!0===t&&this.st.reload()}},{text:"\u5220\u9664",type:"del",click:e=>{this.config.delDataSouce(e.id).subscribe(e=>{this.msgSer.success("\u5220\u9664\u6210\u529f"),this.st.reload()})}}]}]}ngOnInit(){}add(){this.modalHelpr.create({nzTitle:"\u65b0\u589e\u6570\u636e\u6e90",nzContent:this.ds,nzWidth:"80vw",nzOnOk:()=>{this.config.createDataSource(this.dataSource).subscribe(e=>{e&&this.msgSer.success("\u65b0\u589e\u6210\u529f!")})}})}dataSourceChange(e){this.dataSource=e}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(l.a),n.Tb(d.j),n.Tb(o.c),n.Tb(r.e))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-datasource-index"]],viewQuery:function(e,t){var i;1&e&&(n.Tc(C,!0),n.Tc(E,!0)),2&e&&(n.Ac(i=n.ic())&&(t.ds=i.first),n.Ac(i=n.ic())&&(t.st=i.first))},decls:9,vars:3,consts:[[1,"mb-md"],["nz-button","","nzType","primary",2,"float","right",3,"click"],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["ds",""],[3,"dataSource","dataSourceChange"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"nz-card"),n.Zb(1,"div",0),n.Zb(2,"button",1),n.hc("click",(function(){return t.add()})),n.Oc(3,"\u65b0\u5efa"),n.Yb(),n.Yb(),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb(),n.Mc(7,Z,1,1,"ng-template",null,5,n.Nc)}2&e&&(n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns))},directives:[w.a,p.a,g.a,y.a,f.b,z.a,O.a],encapsulation:2}),e})();var B=i("/SA8"),N=i("L7yB"),M=i("ZyQt"),j=i("3DdR");const P=["moreDs"],A=["moreEs"],R=["sf"],F=["multFunctionOptions"];function L(e,t){1&e&&n.Ub(0,"nz-spin",8)}const Q=function(){return{"overflow-y":"scroll","max-height":"300px"}};function $(e,t){if(1&e){const e=n.ac();n.Zb(0,"fb-tableinfo",18),n.hc("TableNamesChange",(function(t){return n.Ec(e),n.kc(3).tableNameChange(t)})),n.Yb()}if(2&e){const e=n.kc(3);n.sc("Style",n.vc(4,Q))("Datas",e.tableDto)("PickerType",e.pickType)("tableNames",e.tableNames)}}function q(e,t){if(1&e&&n.Mc(0,$,1,5,"fb-tableinfo",17),2&e){const e=n.kc(2);n.sc("ngIf",e.tableDto)}}function W(e,t){if(1&e&&(n.Zb(0,"nz-tag",20),n.Oc(1),n.Yb()),2&e){const e=t.$implicit;n.Db(1),n.Pc(e)}}function G(e,t){if(1&e&&n.Mc(0,W,2,1,"nz-tag",19),2&e){const e=t.$implicit;n.sc("ngForOf",null==e.value?null:e.value.split(","))}}function U(e,t){if(1&e&&(n.Zb(0,"nz-tag",22),n.Oc(1),n.Yb()),2&e){const e=t.$implicit;n.Db(1),n.Pc(e)}}function H(e,t){if(1&e&&n.Mc(0,U,2,1,"nz-tag",21),2&e){const e=t.$implicit;n.sc("ngForOf",null==e.value?null:e.value.split(","))}}function J(e,t){if(1&e){const e=n.ac();n.Zb(0,"sf",9,10),n.Mc(2,q,1,1,"ng-template",11),n.Mc(3,G,1,1,"ng-template",12),n.Mc(4,H,1,1,"ng-template",13),n.Zb(5,"div",14),n.Zb(6,"button",15),n.hc("click",(function(){return n.Ec(e),n.kc().close()})),n.Oc(7,"\u5173\u95ed"),n.Yb(),n.Zb(8,"button",16),n.hc("click",(function(){n.Ec(e);const t=n.Bc(1);return n.kc().save(t.value)})),n.Oc(9," \u4fdd\u5b58 "),n.Yb(),n.Yb(),n.Yb()}if(2&e){const e=n.Bc(1),t=n.kc();n.sc("mode",t.mode)("schema",t.schema)("ui",t.ui)("formData",t.i),n.Db(8),n.sc("disabled",!e.valid)("nzLoading",t.service.client.loading)}}function _(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",23),n.hc("click",(function(){n.Ec(e);const t=n.kc(),i=n.Bc(8);return t.addDataSource(i)})),n.Oc(1,"\u65b0\u589e"),n.Yb()}}function V(e,t){if(1&e){const e=n.ac();n.Zb(0,"fb-datasource",24),n.hc("dataSourceChange",(function(t){return n.Ec(e),n.kc().dataSource=t}))("dataSourceChange",(function(t){return n.Ec(e),n.kc().dataSourceChange(t)})),n.Yb()}if(2&e){const e=n.kc();n.sc("dataSource",e.dataSource)}}function X(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",23),n.hc("click",(function(){n.Ec(e);const t=n.kc(),i=n.Bc(12);return t.addEntitySourrce(i)})),n.Oc(1,"\u65b0\u589e"),n.Yb()}}function K(e,t){if(1&e){const e=n.ac();n.Zb(0,"fb-entitysource",25),n.hc("entitySourceChange",(function(t){return n.Ec(e),n.kc().entitySource=t}))("entitySourceChange",(function(t){return n.Ec(e),n.kc().entitySourceChange(t)})),n.Yb()}if(2&e){const e=n.kc();n.sc("entitySource",e.entitySource)}}let ee=(()=>{class e{constructor(e,t,i,c,n){this.modal=e,this.msgSrv=t,this.service=i,this.modalHelper=c,this.hepler=n,this.mode="default",this.title="\u65b0\u589e\u914d\u7f6e",this.dataSource=new s.c,this.entitySource=new s.d,this.record={},this.i=new s.e,this.pickType=s.f.Ignore,this.ui={"*":{spanLabelFixed:100,grid:{span:12}},$no:{widget:"text"},$href:{widget:"string"},$description:{widget:"textarea",grid:{span:24}}},this.tableNames=[],this.dataSourceId=0}ngOnInit(){this.record.id>0&&(this.mode="edit",console.log(""+this.record.id),this.service.getGeneratorConfig(this.record.id).subscribe(e=>{this.i=e,this.title=`\u4fee\u6539${e.name}\u7684\u914d\u7f6e`}),setTimeout(()=>{const e=0===this.sf.getProperty("/generatorMode").value,t=e?this.sf.getProperty("/dataSourceId").value:this.sf.getProperty("/entitySourceId").value;this.tableNames=this.pickType===s.f.Ignore?this.sf.getProperty("/ignoreTables").value.split(","):this.sf.getProperty("/includeTables").value.split(","),""===this.tableNames[0]&&1===this.tableNames.length&&(this.tableNames=[]),this.previewTable(t,e),console.log(this.multFunctionOptions)},500)),this.dataSourceIdList(),this.schemaInit()}schemaInit(){this.schema={properties:{name:{type:"string",title:"\u540d\u79f0",ui:{change:e=>{this.title=`\u4fee\u6539${e}\u7684\u914d\u7f6e`}}},generatorMode:{type:"number",title:"\u751f\u6210\u5668\u6a21\u5f0f",ui:{widget:"radio",change:e=>{const t=0===e,i=t?this.sf.getProperty("/dataSourceId").value:this.sf.getProperty("/entitySourceId").value;this.previewTable(i,t)}},default:1,enum:[{label:"DbFirst",value:0},{label:"CodeFirst",value:1}]},dataSourceId:{type:"number",title:"\u6570\u636e\u6e90",ui:{widget:"select",dropdownRender:this.moreDs,asyncData:()=>this.service.getDataSourceSelect(),change:e=>{this.dataSourceIdChange(e)}}},entitySourceId:{type:"number",title:"\u5b9e\u4f53\u6e90",ui:{widget:"select",dropdownRender:this.moreEs,asyncData:()=>this.service.getEntitySourceSelect(),change:e=>{this.entitySourceIdChange(e)}}},pickType:{type:"number",title:"\u9009\u62e9\u7c7b\u578b",ui:{widget:"radio",styleType:"button",buttonStyle:"solid",change:e=>{this.pickType=e,this.tableNames=this.pickType===s.f.Ignore?this.sf.getProperty("/ignoreTables").value.split(","):this.sf.getProperty("/includeTables").value.split(","),""===this.tableNames[0]&&1===this.tableNames.length&&(this.tableNames=[])}},default:1,enum:[{label:"\u9009\u4e2d",value:0},{label:"\u5ffd\u7565",value:1}]},preview:{type:"number",title:"\u9884\u89c8",ui:{widget:"custom",grid:{span:24}},default:0},ignoreTables:{type:"string",title:"\u5ffd\u7565\u7684\u8868",ui:{widget:"custom",grid:{span:24},visibleIf:{pickType:e=>1===e},change:e=>{console.log(e)}},default:""},includeTables:{type:"string",title:"\u5305\u542b\u7684\u8868",ui:{widget:"custom",grid:{span:24},visibleIf:{pickType:e=>0===e}},default:""}},if:{properties:{generatorMode:{enum:[0]}}},then:{required:["dataSourceId"]},else:{required:["entitySourceId"]},required:["name","generatorMode"]},console.log(this.schema,"Init")}addDataSource(e){this.modalHelper.create({nzTitle:"\u65b0\u589e\u6570\u636e\u6e90",nzContent:e,nzWidth:"80vw",nzMaskClosable:!1,nzClosable:!1,nzOnOk:()=>{this.service.createDataSource(this.dataSource).subscribe(e=>{if(e){this.msgSrv.success("\u65b0\u589e\u6210\u529f!");const t=this.sf.getProperty("/dataSourceId");this.service.getDataSourceSelect().subscribe(i=>{t.schema.enum=i,this.sf.setValue("/dataSourceId",e.id)})}})}})}dataSourceIdList(){this.service.getDataSourceSelect().subscribe(e=>this.dataSourceSelects=e)}addEntitySourrce(e){this.modalHelper.create({nzTitle:"\u65b0\u589e\u5b9e\u4f53\u6e90",nzContent:e,nzMaskClosable:!1,nzClosable:!1,nzOnOk:()=>{console.log(this.entitySource),this.service.createEntitySource(this.entitySource).subscribe(e=>{if(e){this.msgSrv.success("\u65b0\u589e\u6210\u529f!");const t=this.sf.getProperty("/entitySourceId");this.service.getEntitySourceSelect().subscribe(i=>{t.schema.enum=i,this.sf.setValue("/entitySourceId",e.id)})}})}})}dataSourceChange(e){console.log(e),this.dataSource=e}entitySourceChange(e){this.entitySource=e}dataSourceIdChange(e){this.dataSourceId=e,this.previewTable(e,!0)}entitySourceIdChange(e){this.previewTable(e,!1)}save(e){this.record.id>0?this.service.updateGeneratorConfig(e).subscribe(e=>{this.msgSrv.success("\u4fdd\u5b58\u6210\u529f"),this.modal.close(!0)}):this.service.createGeneratorConfig(e).subscribe(e=>{this.msgSrv.success("\u65b0\u589e\u6210\u529f"),this.modal.close(e.id)})}previewTable(e,t){e>0&&(t?this.service.getDataSource(e).subscribe(e=>{this.hepler.getTableInfo(e).subscribe(e=>{this.tableDto=e})}):this.service.getEntitySource(e).subscribe(e=>{this.hepler.getTableInfo(e).subscribe(e=>{this.tableDto=e})}))}tableNameChange(e){this.pickType===s.f.Ignore?(console.log(e,"ignoreTables"),this.sf.setValue("/ignoreTables",e.join(","))):(console.log(e,"includeTables"),this.sf.setValue("/includeTables",e.join(","))),this.tableNames=e}close(){this.modal.destroy()}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(o.b),n.Tb(r.e),n.Tb(l.a),n.Tb(o.c),n.Tb(B.a))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-config-edit"]],viewQuery:function(e,t){var i;1&e&&(n.Jc(P,!0),n.Jc(A,!0),n.Tc(R,!0),n.Jc(F,!0)),2&e&&(n.Ac(i=n.ic())&&(t.moreDs=i.first),n.Ac(i=n.ic())&&(t.moreEs=i.first),n.Ac(i=n.ic())&&(t.sf=i.first),n.Ac(i=n.ic())&&(t.multFunctionOptions=i.first))},decls:13,vars:3,consts:[[1,"modal-header"],[1,"modal-title"],["class","modal-spin",4,"ngIf"],["button","none",3,"mode","schema","ui","formData",4,"ngIf"],["moreDs",""],["ds",""],["moreEs",""],["es",""],[1,"modal-spin"],["button","none",3,"mode","schema","ui","formData"],["sf",""],["sf-template","preview"],["sf-template","ignoreTables"],["sf-template","includeTables"],[1,"modal-footer"],["nz-button","","type","button",3,"click"],["nz-button","","type","submit","nzType","primary",3,"disabled","nzLoading","click"],[3,"Style","Datas","PickerType","tableNames","TableNamesChange",4,"ngIf"],[3,"Style","Datas","PickerType","tableNames","TableNamesChange"],["nzColor","error",4,"ngFor","ngForOf"],["nzColor","error"],["nzColor","success",4,"ngFor","ngForOf"],["nzColor","success"],["nz-button","","nzType","dashed","nzBlock","",3,"click"],[3,"dataSource","dataSourceChange"],[3,"entitySource","entitySourceChange"]],template:function(e,t){1&e&&(n.Zb(0,"div",0),n.Zb(1,"div",1),n.Oc(2),n.Yb(),n.Yb(),n.Mc(3,L,1,0,"nz-spin",2),n.Mc(4,J,10,6,"sf",3),n.Mc(5,_,2,0,"ng-template",null,4,n.Nc),n.Mc(7,V,1,1,"ng-template",null,5,n.Nc),n.Mc(9,X,2,0,"ng-template",null,6,n.Nc),n.Mc(11,K,1,1,"ng-template",null,7,n.Nc)),2&e&&(n.Db(2),n.Pc(t.title),n.Db(1),n.sc("ngIf",!t.i),n.Db(1),n.sc("ngIf",t.i))},directives:[b.m,m.a,f.b,f.c,p.a,g.a,y.a,N.a,b.l,M.a,O.a,j.a],styles:["[_nghost-%COMP%]     .sf__fixed {\n        flex-flow: wrap;\n      }"]}),e})();const te=["es"],ie=["st"];function ce(e,t){if(1&e){const e=n.ac();n.Zb(0,"fb-entitysource",6),n.hc("entitysourceChange",(function(t){return n.Ec(e),n.kc().entitySource=t}))("entitySourceChange",(function(t){return n.Ec(e),n.kc().entitySourceChange(t)})),n.Yb()}if(2&e){const e=n.kc();n.sc("entitysource",e.entitySource)}}let se=(()=>{class e{constructor(e,t,i,c){this.config=e,this.modal=t,this.modalHelpr=i,this.msgSer=c,this.url="api/config/entitysource",this.entitySource=new s.d,this.searchSchema={properties:{keyword:{type:"string",title:"\u5173\u952e\u5b57"}}},this.columns=[{title:"\u7f16\u53f7",index:"id"},{title:"\u5b9e\u4f53\u6e90\u540d\u79f0",index:"name"},{title:"\u4ece\u54ea\u4e2a\u7a0b\u5e8f\u96c6\u53cd\u5c04\u83b7\u53d6",index:"entityAssemblyName"},{title:"\u57fa\u7c7b",index:"entityBaseName"},{title:"\u64cd\u4f5c",buttons:[{text:"\u7f16\u8f91",type:"modal",modal:{component:j.a,modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"},nzFooter:[{label:"\u786e\u5b9a",show:e=>""!==e.entitySource.name&&void 0!==e.entitySource,onClick:e=>{console.log(e.entitySource,"update"),this.config.updateEntitySource(e.entitySource).subscribe(t=>{this.msgSer.success("\u66f4\u65b0\u6210\u529f"),e.modalRef.close(),this.st.reload()})}}]}},click:(e,t)=>{console.log(e,t,"test"),!0===t&&this.st.reload()}},{text:"\u5220\u9664",type:"del",click:e=>{this.config.delEntitySource(e.id).subscribe(e=>{this.msgSer.success("\u5220\u9664\u6210\u529f"),this.st.reload()})}}]}]}ngOnInit(){}add(){this.modalHelpr.create({nzTitle:"\u65b0\u589e\u6570\u636e\u6e90",nzContent:this.es,nzWidth:"80vw",nzOnOk:()=>{this.config.createEntitySource(this.entitySource).subscribe(e=>{e&&(this.msgSer.success("\u65b0\u589e\u6210\u529f!"),this.st.reload())})}})}entitySourceChange(e){console.log(e,"change"),this.entitySource=e}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(l.a),n.Tb(d.j),n.Tb(o.c),n.Tb(r.e))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-entitysource-index"]],viewQuery:function(e,t){var i;1&e&&(n.Jc(te,!0),n.Tc(ie,!0)),2&e&&(n.Ac(i=n.ic())&&(t.es=i.first),n.Ac(i=n.ic())&&(t.st=i.first))},decls:9,vars:3,consts:[[1,"mb-md"],["nz-button","","nzType","primary",2,"float","right",3,"click"],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["es",""],[3,"entitysource","entitysourceChange","entitySourceChange"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"nz-card"),n.Zb(1,"div",0),n.Zb(2,"button",1),n.hc("click",(function(){return t.add()})),n.Oc(3,"\u65b0\u5efa"),n.Yb(),n.Yb(),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb(),n.Mc(7,ce,1,1,"ng-template",null,5,n.Nc)}2&e&&(n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns))},directives:[w.a,p.a,g.a,y.a,f.b,z.a,j.a],encapsulation:2}),e})();const ne=["st"];function oe(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",5),n.hc("click",(function(){return n.Ec(e),n.kc().checkDataSource()})),n.Oc(1,"\u6570\u636e\u6e90"),n.Yb(),n.Zb(2,"button",5),n.hc("click",(function(){return n.Ec(e),n.kc().checkEntitySource()})),n.Oc(3,"\u5b9e\u4f53\u6e90"),n.Yb(),n.Zb(4,"button",6),n.hc("click",(function(){return n.Ec(e),n.kc().add()})),n.Oc(5,"\u65b0\u5efa"),n.Yb()}}let re=(()=>{class e{constructor(e,t,i){this.config=e,this.modal=t,this.msgSer=i,this.url="api/config",this.searchSchema={properties:{keyword:{type:"string",title:"\u5173\u952e\u5b57"}}},this.columns=[{title:"\u7f16\u53f7",index:"id"},{title:"\u914d\u7f6e\u540d\u79f0",index:"name"},{title:"\u914d\u7f6e\u7c7b\u578b",type:"enum",enum:{0:"DbFirst",1:"CodeFirst"},index:"generatorMode"},{title:"\u9009\u4e2d\u6a21\u5f0f",type:"enum",enum:{0:"\u9009\u4e2d",1:"\u5ffd\u7565"},index:"pickType"},{title:"\u64cd\u4f5c",buttons:[{text:"\u7f16\u8f91",type:"modal",modal:{component:ee,modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}},click:(e,t)=>{!0===t&&this.st.reload()}},{text:"\u5220\u9664",type:"del",click:e=>{this.config.delConfig(e.id).subscribe(e=>{this.msgSer.success("\u5220\u9664\u6210\u529f"),this.st.reload()})}}]}]}checkDataSource(){this.modal.create(Y,{},{modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}}).subscribe(()=>this.st.reload())}checkEntitySource(){this.modal.create(se,{},{modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}}).subscribe(()=>this.st.reload())}ngOnInit(){}add(){this.modal.createStatic(ee,{i:{id:0}},{modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}}).subscribe(()=>this.st.reload())}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(l.a),n.Tb(d.j),n.Tb(r.e))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-config"]],viewQuery:function(e,t){var i;1&e&&n.Tc(ne,!0),2&e&&n.Ac(i=n.ic())&&(t.st=i.first)},decls:7,vars:4,consts:[[3,"action"],["phActionTpl",""],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["nz-button","","nzType","default",3,"click"],["nz-button","","nzType","primary",3,"click"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"page-header",0),n.Mc(1,oe,6,0,"ng-template",null,1,n.Nc),n.Yb(),n.Zb(3,"nz-card"),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb()}if(2&e){const e=n.Bc(2);n.sc("action",e),n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns)}},directives:[T.a,w.a,f.b,z.a,p.a,g.a,y.a],encapsulation:2}),e})();var ae=i("tyNb");class le{constructor(e=10,t=1,i="a.id asc",c="",s=0){this.pageSize=e,this.pageNumber=t,this.sortFields=i,this.keyword=c,this.total=s}}const ue=["moreConfig"],de=["sf"],he=["deleteBtn"];function be(e,t){1&e&&n.Ub(0,"nz-spin",6)}function me(e,t){if(1&e){const e=n.ac();n.Zb(0,"sf",7,8),n.hc("formSubmit",(function(t){return n.Ec(e),n.kc().save(t)})),n.Yb()}if(2&e){const e=n.kc();n.sc("schema",e.schema)("ui",e.ui)("formData",e.i)}}function fe(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",9),n.hc("click",(function(){return n.Ec(e),n.kc().newConfig()})),n.Oc(1,"\u65b0\u589e\u914d\u7f6e"),n.Yb()}}function pe(e,t){1&e&&(n.Zb(0,"button",10),n.Oc(1,"\u5220\u9664\u914d\u7f6e"),n.Yb())}let ge=(()=>{class e{constructor(e,t,i,c,s,n){this.modal=e,this.modalHelper=t,this.msgSrv=i,this.projectService=c,this.builderService=s,this.configService=n,this.record={},this.Title="\u65b0\u589e\u9879\u76ee",this.ui={"*":{spanLabelFixed:100,grid:{span:12}},$generatorModeConfigId:{ui:{spanControl:9},grid:{span:24}},$projectBuilders:{items:{properties:{"*":{ui:{spanControl:12}}}}}}}ngOnInit(){this.record.id>0&&this.projectService.getProject(this.record.id).subscribe(e=>{this.i=e,this.Title="\u7f16\u8f91\u9879\u76ee:"+e.projectInfo.nameSpace}),this.schema=this.SchemaInit()}SchemaInit(){return{properties:{projectInfo:{title:"\u9879\u76ee\u4fe1\u606f",type:"object",ui:{type:"card",grid:{span:24}},properties:{nameSpace:{type:"string",title:"\u9879\u76ee\u540d\u79f0",description:"\u9879\u76ee\u7684\u540d\u79f0",ui:{change:e=>{this.Title="\u7f16\u8f91\u9879\u76ee"+e}}},author:{type:"string",title:"\u4f5c\u8005",description:"\u9879\u76ee\u4f5c\u8005"},rootPath:{type:"string",title:"\u7269\u7406\u6839\u8def\u5f84",description:"\u6700\u7ec8\u4f1a\u8f93\u51fa\u5230\u7684\u7269\u7406\u8def\u5f84"}}},generatorModeConfigId:{type:"number",title:"\u751f\u6210\u5668\u914d\u7f6e",ui:{widget:"select",dropdownRender:this.moreConfig,suffixIcon:this.deleteBtn,asyncData:()=>this.configService.getGeneratorConfigSelect()}},buildersId:{type:"number",title:"\u6784\u5efa\u5668",uniqueItems:!0,ui:{widget:"transfer",titles:["\u672a\u9009\u4e2d","\u9009\u4e2d"],grid:{span:24},asyncData:()=>this.builderService.getBuilderSelect(s.b.Builder)}},globalBuildersId:{type:"number",title:"\u5168\u8868\u6784\u5efa\u5668",uniqueItems:!0,ui:{widget:"transfer",titles:["\u672a\u9009\u4e2d","\u9009\u4e2d"],grid:{span:24},asyncData:()=>this.builderService.getBuilderSelect(s.b.GlobalBuilder)}},_buildersId:{type:"array",ui:{hidden:!0}}},required:["projectInfo.projectName","projectInfo.author","projectInfo.outPutPath","projectInfo.rootPath"]}}newConfig(){this.modalHelper.createStatic(ee,{i:{id:0}},{modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}}).subscribe(e=>{const t=this.sf.getProperty("/generatorModeConfigId");this.configService.getGeneratorConfigSelect().subscribe(i=>{t.schema.enum=i,this.sf.setValue("/generatorModeConfigId",e)})})}save(e){this.record.id>0?this.projectService.updateProject(e).subscribe(e=>{this.msgSrv.success("\u4fdd\u5b58\u6210\u529f"),this.modal.close(!0)}):this.projectService.createProject(e).subscribe(e=>{this.msgSrv.success("\u65b0\u589e\u6210\u529f"),this.modal.close(!0)})}close(){this.modal.destroy()}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(o.b),n.Tb(d.j),n.Tb(r.e),n.Tb(h.a),n.Tb(a.a),n.Tb(l.a))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-project-edit"]],viewQuery:function(e,t){var i;1&e&&(n.Jc(ue,!0),n.Tc(de,!0),n.Tc(he,!0)),2&e&&(n.Ac(i=n.ic())&&(t.moreConfig=i.first),n.Ac(i=n.ic())&&(t.sf=i.first),n.Ac(i=n.ic())&&(t.deleteBtn=i.first))},decls:9,vars:3,consts:[[1,"modal-header"],[1,"modal-title"],["class","modal-spin",4,"ngIf"],["mode","edit",3,"schema","ui","formData","formSubmit",4,"ngIf"],["moreConfig",""],["deleteBtn",""],[1,"modal-spin"],["mode","edit",3,"schema","ui","formData","formSubmit"],["sf",""],["nz-button","","nzType","dashed","nzBlock","",3,"click"],["nz-button","","nzType","danger"]],template:function(e,t){1&e&&(n.Zb(0,"div",0),n.Zb(1,"div",1),n.Oc(2),n.Yb(),n.Yb(),n.Mc(3,be,1,0,"nz-spin",2),n.Mc(4,me,2,3,"sf",3),n.Mc(5,fe,2,0,"ng-template",null,4,n.Nc),n.Mc(7,pe,2,0,"ng-template",null,5,n.Nc)),2&e&&(n.Db(2),n.Pc(t.Title),n.Db(1),n.sc("ngIf",!t.i),n.Db(1),n.sc("ngIf",t.i))},directives:[b.m,m.a,f.b,p.a,g.a,y.a],styles:["[_nghost-%COMP%]     .sf__fixed {\n        flex-flow: wrap;\n      }"]}),e})();const ye=["st"];function Se(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",5),n.hc("click",(function(){return n.Ec(e),n.kc().add()})),n.Oc(1,"\u65b0\u5efa"),n.Yb()}}const ve=["st"];function ke(e,t){if(1&e){const e=n.ac();n.Zb(0,"button",5),n.hc("click",(function(){return n.Ec(e),n.kc().add()})),n.Oc(1,"\u65b0\u5efa"),n.Yb()}}const Te=[{path:"index",component:(()=>{class e{constructor(e,t,i){this.projectService=e,this.modal=t,this.msgSer=i,this.url="api/project/page",this.res={reName:{list:["datas"]},process:(e,t)=>(console.log(t),e)},this.searchSchema={properties:{keyword:{type:"string",title:"\u5173\u952e\u5b57"}}},this.columns=[{title:"\u7f16\u53f7",index:"id"},{title:"\u9879\u76ee\u540d\u79f0",index:"projectInfo.nameSpace"},{title:"\u914d\u7f6e\u540d\u79f0",index:"generatorModeConfig.name"},{title:"\u64cd\u4f5c",buttons:[{text:"\u751f\u6210",type:"link",click:e=>{this.projectService.buildTask(e.id).subscribe(e=>{this.msgSer.success("\u751f\u6210\u6210\u529f"),this.st.reload()})}},{icon:"edit",text:"\u7f16\u8f91",type:"modal",modal:{component:ge},click:(e,t)=>{!0===t&&this.st.reload()}},{text:"\u5220\u9664",type:"del",click:e=>{this.projectService.deleteProject(e.id).subscribe(e=>{this.msgSer.success("\u5220\u9664\u6210\u529f"),this.st.reload()})}}]}],this.page=new le}ngOnInit(){}add(){this.modal.createStatic(ge,{i:{id:0}},{modalOptions:{nzWidth:"80vw",nzBodyStyle:{"overflow-y":"scroll","max-height":"70vh"}}}).subscribe(()=>{console.log("\u5237\u65b0"),this.st.reload()})}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(h.a),n.Tb(d.j),n.Tb(r.e))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-project"]],viewQuery:function(e,t){var i;1&e&&n.Tc(ye,!0),2&e&&n.Ac(i=n.ic())&&(t.st=i.first)},decls:7,vars:4,consts:[[3,"action"],["phActionTpl",""],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["nz-button","","nzType","primary",3,"click"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"page-header",0),n.Mc(1,Se,2,0,"ng-template",null,1,n.Nc),n.Yb(),n.Zb(3,"nz-card"),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb()}if(2&e){const e=n.Bc(2);n.sc("action",e),n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns)}},directives:[T.a,w.a,f.b,z.a,p.a,g.a,y.a],encapsulation:2}),e})()},{path:"config",component:re},{path:"builder",component:D},{path:"template",component:(()=>{class e{constructor(e,t){this.http=e,this.modal=t,this.url="/user",this.searchSchema={properties:{no:{type:"string",title:"\u7f16\u53f7"}}},this.columns=[{title:"\u7f16\u53f7",index:"no"},{title:"\u8c03\u7528\u6b21\u6570",type:"number",index:"callNo"},{title:"\u5934\u50cf",type:"img",width:"50px",index:"avatar"},{title:"\u65f6\u95f4",type:"date",index:"updatedAt"},{title:"",buttons:[]}]}ngOnInit(){}add(){}}return e.\u0275fac=function(t){return new(t||e)(n.Tb(d.r),n.Tb(d.j))},e.\u0275cmp=n.Nb({type:e,selectors:[["fb-generator-template"]],viewQuery:function(e,t){var i;1&e&&n.Tc(ve,!0),2&e&&n.Ac(i=n.ic())&&(t.st=i.first)},decls:7,vars:4,consts:[[3,"action"],["phActionTpl",""],["mode","search",3,"schema","formSubmit","formReset"],[3,"data","columns"],["st",""],["nz-button","","nzType","primary",3,"click"]],template:function(e,t){if(1&e){const e=n.ac();n.Zb(0,"page-header",0),n.Mc(1,ke,2,0,"ng-template",null,1,n.Nc),n.Yb(),n.Zb(3,"nz-card"),n.Zb(4,"sf",2),n.hc("formSubmit",(function(t){return n.Ec(e),n.Bc(6).reset(t)}))("formReset",(function(t){return n.Ec(e),n.Bc(6).reset(t)})),n.Yb(),n.Ub(5,"st",3,4),n.Yb()}if(2&e){const e=n.Bc(2);n.sc("action",e),n.Db(4),n.sc("schema",t.searchSchema),n.Db(1),n.sc("data",t.url)("columns",t.columns)}},directives:[T.a,w.a,f.b,z.a,p.a,g.a,y.a],encapsulation:2}),e})()}];let we=(()=>{class e{}return e.\u0275mod=n.Rb({type:e}),e.\u0275inj=n.Qb({factory:function(t){return new(t||e)},imports:[[ae.n.forChild(Te)],ae.n]}),e})(),ze=(()=>{class e{}return e.\u0275mod=n.Rb({type:e}),e.\u0275inj=n.Qb({factory:function(t){return new(t||e)},imports:[[c.a,we]]}),e})()}}]);