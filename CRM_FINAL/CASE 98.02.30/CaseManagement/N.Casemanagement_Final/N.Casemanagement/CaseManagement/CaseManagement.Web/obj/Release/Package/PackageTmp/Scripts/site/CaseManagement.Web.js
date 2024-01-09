﻿var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var CalendarEventDialog = (function (_super) {
            __extends(CalendarEventDialog, _super);
            function CalendarEventDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.CalendarEventForm(this.idPrefix);
            }
            CalendarEventDialog.prototype.getFormKey = function () { return Administration.CalendarEventForm.formKey; };
            CalendarEventDialog.prototype.getIdProperty = function () { return Administration.CalendarEventRow.idProperty; };
            CalendarEventDialog.prototype.getLocalTextPrefix = function () { return Administration.CalendarEventRow.localTextPrefix; };
            CalendarEventDialog.prototype.getNameProperty = function () { return Administration.CalendarEventRow.nameProperty; };
            CalendarEventDialog.prototype.getService = function () { return Administration.CalendarEventService.baseUrl; };
            CalendarEventDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], CalendarEventDialog);
            return CalendarEventDialog;
        }(Serenity.EntityDialog));
        Administration.CalendarEventDialog = CalendarEventDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var CalendarEventGrid = (function (_super) {
            __extends(CalendarEventGrid, _super);
            function CalendarEventGrid(container) {
                _super.call(this, container);
            }
            CalendarEventGrid.prototype.getColumnsKey = function () { return 'Administration.CalendarEvent'; };
            CalendarEventGrid.prototype.getDialogType = function () { return Administration.CalendarEventDialog; };
            CalendarEventGrid.prototype.getIdProperty = function () { return Administration.CalendarEventRow.idProperty; };
            CalendarEventGrid.prototype.getLocalTextPrefix = function () { return Administration.CalendarEventRow.localTextPrefix; };
            CalendarEventGrid.prototype.getService = function () { return Administration.CalendarEventService.baseUrl; };
            CalendarEventGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], CalendarEventGrid);
            return CalendarEventGrid;
        }(Serenity.EntityGrid));
        Administration.CalendarEventGrid = CalendarEventGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LanguageDialog = (function (_super) {
            __extends(LanguageDialog, _super);
            function LanguageDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.LanguageForm(this.idPrefix);
            }
            LanguageDialog.prototype.getFormKey = function () { return Administration.LanguageForm.formKey; };
            LanguageDialog.prototype.getIdProperty = function () { return Administration.LanguageRow.idProperty; };
            LanguageDialog.prototype.getLocalTextPrefix = function () { return Administration.LanguageRow.localTextPrefix; };
            LanguageDialog.prototype.getNameProperty = function () { return Administration.LanguageRow.nameProperty; };
            LanguageDialog.prototype.getService = function () { return Administration.LanguageService.baseUrl; };
            LanguageDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], LanguageDialog);
            return LanguageDialog;
        }(Serenity.EntityDialog));
        Administration.LanguageDialog = LanguageDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LanguageGrid = (function (_super) {
            __extends(LanguageGrid, _super);
            function LanguageGrid(container) {
                _super.call(this, container);
            }
            LanguageGrid.prototype.getColumnsKey = function () { return "Administration.Language"; };
            LanguageGrid.prototype.getDialogType = function () { return Administration.LanguageDialog; };
            LanguageGrid.prototype.getIdProperty = function () { return Administration.LanguageRow.idProperty; };
            LanguageGrid.prototype.getLocalTextPrefix = function () { return Administration.LanguageRow.localTextPrefix; };
            LanguageGrid.prototype.getService = function () { return Administration.LanguageService.baseUrl; };
            LanguageGrid.prototype.getDefaultSortBy = function () {
                return [Administration.LanguageRow.Fields.LanguageName];
            };
            LanguageGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], LanguageGrid);
            return LanguageGrid;
        }(Serenity.EntityGrid));
        Administration.LanguageGrid = LanguageGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LogDialog = (function (_super) {
            __extends(LogDialog, _super);
            function LogDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.LogForm(this.idPrefix);
            }
            LogDialog.prototype.getFormKey = function () { return Administration.LogForm.formKey; };
            LogDialog.prototype.getIdProperty = function () { return Administration.LogRow.idProperty; };
            LogDialog.prototype.getLocalTextPrefix = function () { return Administration.LogRow.localTextPrefix; };
            LogDialog.prototype.getNameProperty = function () { return Administration.LogRow.nameProperty; };
            LogDialog.prototype.getService = function () { return Administration.LogService.baseUrl; };
            LogDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], LogDialog);
            return LogDialog;
        }(Serenity.EntityDialog));
        Administration.LogDialog = LogDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LogGrid = (function (_super) {
            __extends(LogGrid, _super);
            function LogGrid(container) {
                _super.call(this, container);
            }
            LogGrid.prototype.getColumnsKey = function () { return 'Administration.Log'; };
            LogGrid.prototype.getDialogType = function () { return Administration.LogDialog; };
            LogGrid.prototype.getIdProperty = function () { return Administration.LogRow.idProperty; };
            LogGrid.prototype.getLocalTextPrefix = function () { return Administration.LogRow.localTextPrefix; };
            LogGrid.prototype.getService = function () { return Administration.LogService.baseUrl; };
            LogGrid.prototype.getButtons = function () {
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var Logtime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(Logtime_Start);
                        var Logtime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(Logtime_End);
                        var Action = document.getElementById("select2-chosen-1").innerHTML;
                        if (Action == null) {
                            Action = "";
                        }
                        var User = document.getElementById("select2-chosen-2").innerHTML;
                        if (User == null) {
                            User = "";
                        }
                        var Province = document.getElementById("select2-chosen-3").innerHTML;
                        if (Province == null) {
                            Province = "";
                        }
                        window.location.href = "../Common/LogPrint?Logtime_Start=" + Logtime_Start + "&Logtime_End=" + Logtime_End + "&Action=" + Action + "&User=" + User + "&Province=" + Province;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            LogGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], LogGrid);
            return LogGrid;
        }(Serenity.EntityGrid));
        Administration.LogGrid = LogGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationDialog = (function (_super) {
            __extends(NotificationDialog, _super);
            function NotificationDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.NotificationForm(this.idPrefix);
            }
            NotificationDialog.prototype.getFormKey = function () { return Administration.NotificationForm.formKey; };
            NotificationDialog.prototype.getIdProperty = function () { return Administration.NotificationRow.idProperty; };
            NotificationDialog.prototype.getLocalTextPrefix = function () { return Administration.NotificationRow.localTextPrefix; };
            NotificationDialog.prototype.getNameProperty = function () { return Administration.NotificationRow.nameProperty; };
            NotificationDialog.prototype.getService = function () { return Administration.NotificationService.baseUrl; };
            NotificationDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], NotificationDialog);
            return NotificationDialog;
        }(Serenity.EntityDialog));
        Administration.NotificationDialog = NotificationDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGrid = (function (_super) {
            __extends(NotificationGrid, _super);
            function NotificationGrid(container) {
                _super.call(this, container);
            }
            NotificationGrid.prototype.getColumnsKey = function () { return 'Administration.Notification'; };
            NotificationGrid.prototype.getDialogType = function () { return Administration.NotificationDialog; };
            NotificationGrid.prototype.getIdProperty = function () { return Administration.NotificationRow.idProperty; };
            NotificationGrid.prototype.getLocalTextPrefix = function () { return Administration.NotificationRow.localTextPrefix; };
            NotificationGrid.prototype.getService = function () { return Administration.NotificationService.baseUrl; };
            NotificationGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], NotificationGrid);
            return NotificationGrid;
        }(Serenity.EntityGrid));
        Administration.NotificationGrid = NotificationGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGroupDialog = (function (_super) {
            __extends(NotificationGroupDialog, _super);
            function NotificationGroupDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.NotificationGroupForm(this.idPrefix);
            }
            NotificationGroupDialog.prototype.getFormKey = function () { return Administration.NotificationGroupForm.formKey; };
            NotificationGroupDialog.prototype.getIdProperty = function () { return Administration.NotificationGroupRow.idProperty; };
            NotificationGroupDialog.prototype.getLocalTextPrefix = function () { return Administration.NotificationGroupRow.localTextPrefix; };
            NotificationGroupDialog.prototype.getNameProperty = function () { return Administration.NotificationGroupRow.nameProperty; };
            NotificationGroupDialog.prototype.getService = function () { return Administration.NotificationGroupService.baseUrl; };
            NotificationGroupDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], NotificationGroupDialog);
            return NotificationGroupDialog;
        }(Serenity.EntityDialog));
        Administration.NotificationGroupDialog = NotificationGroupDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGroupGrid = (function (_super) {
            __extends(NotificationGroupGrid, _super);
            function NotificationGroupGrid(container) {
                _super.call(this, container);
            }
            NotificationGroupGrid.prototype.getColumnsKey = function () { return 'Administration.NotificationGroup'; };
            NotificationGroupGrid.prototype.getDialogType = function () { return Administration.NotificationGroupDialog; };
            NotificationGroupGrid.prototype.getIdProperty = function () { return Administration.NotificationGroupRow.idProperty; };
            NotificationGroupGrid.prototype.getLocalTextPrefix = function () { return Administration.NotificationGroupRow.localTextPrefix; };
            NotificationGroupGrid.prototype.getService = function () { return Administration.NotificationGroupService.baseUrl; };
            NotificationGroupGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], NotificationGroupGrid);
            return NotificationGroupGrid;
        }(Serenity.EntityGrid));
        Administration.NotificationGroupGrid = NotificationGroupGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleDialog = (function (_super) {
            __extends(RoleDialog, _super);
            function RoleDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.RoleForm(this.idPrefix);
            }
            RoleDialog.prototype.getFormKey = function () { return Administration.RoleForm.formKey; };
            RoleDialog.prototype.getIdProperty = function () { return Administration.RoleRow.idProperty; };
            RoleDialog.prototype.getLocalTextPrefix = function () { return Administration.RoleRow.localTextPrefix; };
            RoleDialog.prototype.getNameProperty = function () { return Administration.RoleRow.nameProperty; };
            RoleDialog.prototype.getService = function () { return Administration.RoleService.baseUrl; };
            RoleDialog.prototype.getToolbarButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.push({
                    title: Q.text('Site.RolePermissionDialog.EditButton'),
                    cssClass: 'edit-permissions-button',
                    icon: 'icon-lock-open text-green',
                    onClick: function () {
                        new Administration.RolePermissionDialog({
                            roleID: _this.entity.RoleId,
                            title: _this.entity.RoleName
                        }).dialogOpen();
                    }
                });
                buttons.push({
                    title: Q.text('ویرایش مراحل'),
                    cssClass: 'edit-permissions-button',
                    icon: 'icon-lock-open text-green',
                    onClick: function () {
                        new Administration.RoleStepDialog({
                            roleID: _this.entity.RoleId,
                            title: _this.entity.RoleName
                        }).dialogOpen();
                    }
                });
                return buttons;
            };
            RoleDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                this.toolbar.findButton("edit-permissions-button").toggleClass("disabled", this.isNewOrDeleted());
            };
            RoleDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], RoleDialog);
            return RoleDialog;
        }(Serenity.EntityDialog));
        Administration.RoleDialog = RoleDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleGrid = (function (_super) {
            __extends(RoleGrid, _super);
            function RoleGrid(container) {
                _super.call(this, container);
            }
            RoleGrid.prototype.getColumnsKey = function () { return "Administration.Role"; };
            RoleGrid.prototype.getDialogType = function () { return Administration.RoleDialog; };
            RoleGrid.prototype.getIdProperty = function () { return Administration.RoleRow.idProperty; };
            RoleGrid.prototype.getLocalTextPrefix = function () { return Administration.RoleRow.localTextPrefix; };
            RoleGrid.prototype.getService = function () { return Administration.RoleService.baseUrl; };
            RoleGrid.prototype.getDefaultSortBy = function () {
                return [Administration.RoleRow.Fields.RoleName];
            };
            RoleGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], RoleGrid);
            return RoleGrid;
        }(Serenity.EntityGrid));
        Administration.RoleGrid = RoleGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RolePermissionDialog = (function (_super) {
            __extends(RolePermissionDialog, _super);
            function RolePermissionDialog(opt) {
                var _this = this;
                _super.call(this, opt);
                this.permissions = new Administration.PermissionCheckEditor(this.byId('Permissions'), {
                    showRevoke: false
                });
                Administration.RolePermissionService.List({
                    RoleID: this.options.roleID,
                    Module: null,
                    Submodule: null
                }, function (response) {
                    _this.permissions.set_value(response.Entities.map(function (x) { return ({ PermissionKey: x }); }));
                });
            }
            RolePermissionDialog.prototype.getDialogOptions = function () {
                var _this = this;
                var opt = _super.prototype.getDialogOptions.call(this);
                opt.buttons = [
                    {
                        text: Q.text('Dialogs.OkButton'),
                        click: function (e) {
                            Administration.RolePermissionService.Update({
                                RoleID: _this.options.roleID,
                                Permissions: _this.permissions.get_value().map(function (x) { return x.PermissionKey; }),
                                Module: null,
                                Submodule: null
                            }, function (response) {
                                _this.dialogClose();
                                window.setTimeout(function () { return Q.notifySuccess(Q.text('Site.RolePermissionDialog.SaveSuccess')); }, 0);
                            });
                        }
                    }, {
                        text: Q.text('Dialogs.CancelButton'),
                        click: function () { return _this.dialogClose(); }
                    }];
                opt.title = Q.format(Q.text('Site.RolePermissionDialog.DialogTitle'), this.options.title);
                return opt;
            };
            RolePermissionDialog.prototype.getTemplate = function () {
                return '<div id="~_Permissions"></div>';
            };
            RolePermissionDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], RolePermissionDialog);
            return RolePermissionDialog;
        }(Serenity.TemplatedDialog));
        Administration.RolePermissionDialog = RolePermissionDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleStepDialog = (function (_super) {
            __extends(RoleStepDialog, _super);
            function RoleStepDialog(opt) {
                var _this = this;
                _super.call(this, opt);
                this.steps = new Administration.StepCheckEditor(this.byId('Steps'));
                Administration.RoleStepService.List({
                    RoleID: this.options.roleID
                }, function (response) {
                    _this.steps.value = response.Entities.map(function (x) { return x.toString(); });
                });
            }
            RoleStepDialog.prototype.getDialogOptions = function () {
                var _this = this;
                var opt = _super.prototype.getDialogOptions.call(this);
                opt.buttons = [{
                        text: Q.text('Dialogs.OkButton'),
                        click: function () {
                            Q.serviceRequest('Administration/RoleStep/Update', {
                                RoleID: _this.options.roleID,
                                Steps: _this.steps.value.map(function (x) { return parseInt(x, 10); })
                            }, function (response) {
                                _this.dialogClose();
                                Q.notifySuccess(Q.text('Site.RoleStepDialog.SaveSuccess'));
                            });
                        }
                    }, {
                        text: Q.text('Dialogs.CancelButton'),
                        click: function () { return _this.dialogClose(); }
                    }];
                opt.title = Q.format(Q.text('Site.RoleStepDialog.DialogTitle'), this.options.title);
                return opt;
            };
            RoleStepDialog.prototype.getTemplate = function () {
                return '<div id="~_Steps"></div>';
            };
            RoleStepDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], RoleStepDialog);
            return RoleStepDialog;
        }(Serenity.TemplatedDialog));
        Administration.RoleStepDialog = RoleStepDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var StepCheckEditor = (function (_super) {
            __extends(StepCheckEditor, _super);
            function StepCheckEditor(div) {
                _super.call(this, div);
            }
            StepCheckEditor.prototype.createToolbarExtensions = function () {
                var _this = this;
                _super.prototype.createToolbarExtensions.call(this);
                Serenity.GridUtils.addQuickSearchInputCustom(this.toolbar.element, function (field, text) {
                    _this.searchText = Select2.util.stripDiacritics(text || '').toUpperCase();
                    _this.view.setItems(_this.view.getItems(), true);
                });
            };
            StepCheckEditor.prototype.getButtons = function () {
                return [];
            };
            StepCheckEditor.prototype.getTreeItems = function () {
                return CaseManagement.WorkFlow.WorkFlowStepRow.getLookup().items.map(function (step) { return {
                    id: step.Id.toString(),
                    text: step.Name
                }; });
            };
            StepCheckEditor.prototype.onViewFilter = function (item) {
                return _super.prototype.onViewFilter.call(this, item) &&
                    (Q.isEmptyOrNull(this.searchText) ||
                        Select2.util.stripDiacritics(item.text || '')
                            .toUpperCase().indexOf(this.searchText) >= 0);
            };
            StepCheckEditor = __decorate([
                Serenity.Decorators.registerEditor()
            ], StepCheckEditor);
            return StepCheckEditor;
        }(Serenity.CheckTreeEditor));
        Administration.StepCheckEditor = StepCheckEditor;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var TranslationGrid = (function (_super) {
            __extends(TranslationGrid, _super);
            function TranslationGrid(container) {
                var _this = this;
                _super.call(this, container);
                this.element.on('keyup.' + this.uniqueName + ' change.' + this.uniqueName, 'input.custom-text', function (e) {
                    var value = Q.trimToNull($(e.target).val());
                    if (value === '') {
                        value = null;
                    }
                    _this.view.getItemById($(e.target).data('key')).CustomText = value;
                    _this.hasChanges = true;
                });
            }
            TranslationGrid.prototype.getIdProperty = function () { return "Key"; };
            TranslationGrid.prototype.getLocalTextPrefix = function () { return "Administration.Translation"; };
            TranslationGrid.prototype.getService = function () { return Administration.TranslationService.baseUrl; };
            TranslationGrid.prototype.onClick = function (e, row, cell) {
                var _this = this;
                _super.prototype.onClick.call(this, e, row, cell);
                if (e.isDefaultPrevented()) {
                    return;
                }
                var item = this.itemAt(row);
                var done;
                if ($(e.target).hasClass('source-text')) {
                    e.preventDefault();
                    done = function () {
                        item.CustomText = item.SourceText;
                        _this.view.updateItem(item.Key, item);
                        _this.hasChanges = true;
                    };
                    if (Q.isTrimmedEmpty(item.CustomText) ||
                        (Q.trimToEmpty(item.CustomText) === Q.trimToEmpty(item.SourceText))) {
                        done();
                        return;
                    }
                    Q.confirm(Q.text('Db.Administration.Translation.OverrideConfirmation'), done);
                    return;
                }
                if ($(e.target).hasClass('target-text')) {
                    e.preventDefault();
                    done = function () {
                        item.CustomText = item.TargetText;
                        _this.view.updateItem(item.Key, item);
                        _this.hasChanges = true;
                    };
                    if (Q.isTrimmedEmpty(item.CustomText) ||
                        (Q.trimToEmpty(item.CustomText) === Q.trimToEmpty(item.TargetText))) {
                        done();
                        return;
                    }
                    Q.confirm(Q.text('Db.Administration.Translation.OverrideConfirmation'), done);
                    return;
                }
            };
            TranslationGrid.prototype.getColumns = function () {
                var columns = [];
                columns.push({ field: 'Key', width: 300, sortable: false });
                columns.push({
                    field: 'SourceText',
                    width: 300,
                    sortable: false,
                    format: function (ctx) {
                        return Q.outerHtml($('<a/>')
                            .addClass('source-text')
                            .text(ctx.value || ''));
                    }
                });
                columns.push({
                    field: 'CustomText',
                    width: 300,
                    sortable: false,
                    format: function (ctx) { return Q.outerHtml($('<input/>')
                        .addClass('custom-text')
                        .attr('value', ctx.value)
                        .attr('type', 'text')
                        .attr('data-key', ctx.item.Key)); }
                });
                columns.push({
                    field: 'TargetText',
                    width: 300,
                    sortable: false,
                    format: function (ctx) { return Q.outerHtml($('<a/>')
                        .addClass('target-text')
                        .text(ctx.value || '')); }
                });
                return columns;
            };
            TranslationGrid.prototype.createToolbarExtensions = function () {
                var _this = this;
                _super.prototype.createToolbarExtensions.call(this);
                var opt = {
                    lookupKey: 'Administration.Language'
                };
                this.sourceLanguage = Serenity.Widget.create({
                    type: Serenity.LookupEditor,
                    element: function (el) { return el.appendTo(_this.toolbar.element).attr('placeholder', '--- ' +
                        Q.text('Db.Administration.Translation.SourceLanguage') + ' ---'); },
                    options: opt
                });
                this.sourceLanguage.changeSelect2(function (e) {
                    if (_this.hasChanges) {
                        _this.saveChanges(_this.targetLanguageKey).then(function () { return _this.refresh(); });
                    }
                    else {
                        _this.refresh();
                    }
                });
                this.targetLanguage = Serenity.Widget.create({
                    type: Serenity.LookupEditor,
                    element: function (el) { return el.appendTo(_this.toolbar.element).attr('placeholder', '--- ' +
                        Q.text('Db.Administration.Translation.TargetLanguage') + ' ---'); },
                    options: opt
                });
                this.targetLanguage.changeSelect2(function (e) {
                    if (_this.hasChanges) {
                        _this.saveChanges(_this.targetLanguageKey).then(function () { return _this.refresh(); });
                    }
                    else {
                        _this.refresh();
                    }
                });
            };
            TranslationGrid.prototype.saveChanges = function (language) {
                var _this = this;
                var translations = {};
                for (var _i = 0, _a = this.getItems(); _i < _a.length; _i++) {
                    var item = _a[_i];
                    translations[item.Key] = item.CustomText;
                }
                return RSVP.resolve(Administration.TranslationService.Update({
                    TargetLanguageID: language,
                    Translations: translations
                })).then(function () {
                    _this.hasChanges = false;
                    language = Q.trimToNull(language) || 'invariant';
                    Q.notifySuccess('User translations in "' + language +
                        '" language are saved to "user.texts.' +
                        language + '.json" ' + 'file under "~/App_Data/texts/"', '');
                });
            };
            TranslationGrid.prototype.onViewSubmit = function () {
                var request = this.view.params;
                request.SourceLanguageID = this.sourceLanguage.value;
                this.targetLanguageKey = this.targetLanguage.value || '';
                request.TargetLanguageID = this.targetLanguageKey;
                this.hasChanges = false;
                return _super.prototype.onViewSubmit.call(this);
            };
            TranslationGrid.prototype.getButtons = function () {
                var _this = this;
                return [{
                        title: Q.text('Db.Administration.Translation.SaveChangesButton'),
                        onClick: function (e) { return _this.saveChanges(_this.targetLanguageKey).then(function () { return _this.refresh(); }); },
                        cssClass: 'apply-changes-button'
                    }];
            };
            TranslationGrid.prototype.createQuickSearchInput = function () {
                var _this = this;
                Serenity.GridUtils.addQuickSearchInputCustom(this.toolbar.element, function (field, searchText) {
                    _this.searchText = searchText;
                    _this.view.setItems(_this.view.getItems(), true);
                });
            };
            TranslationGrid.prototype.onViewFilter = function (item) {
                if (!_super.prototype.onViewFilter.call(this, item)) {
                    return false;
                }
                if (!this.searchText) {
                    return true;
                }
                var sd = Select2.util.stripDiacritics;
                var searching = sd(this.searchText).toLowerCase();
                function match(str) {
                    if (!str)
                        return false;
                    return str.toLowerCase().indexOf(searching) >= 0;
                }
                return Q.isEmptyOrNull(searching) || match(item.Key) || match(item.SourceText) ||
                    match(item.TargetText) || match(item.CustomText);
            };
            TranslationGrid.prototype.usePager = function () {
                return false;
            };
            TranslationGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], TranslationGrid);
            return TranslationGrid;
        }(Serenity.EntityGrid));
        Administration.TranslationGrid = TranslationGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserDialog = (function (_super) {
            __extends(UserDialog, _super);
            function UserDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Administration.UserForm(this.idPrefix);
                this.form.Password.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.Password.value.length < 7)
                        return "Password must be at least 7 characters!";
                });
                this.form.PasswordConfirm.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.Password.value != _this.form.PasswordConfirm.value)
                        return "The passwords entered doesn't match!";
                });
            }
            UserDialog.prototype.getFormKey = function () { return Administration.UserForm.formKey; };
            UserDialog.prototype.getIdProperty = function () { return Administration.UserRow.idProperty; };
            UserDialog.prototype.getIsActiveProperty = function () { return Administration.UserRow.isActiveProperty; };
            UserDialog.prototype.getLocalTextPrefix = function () { return Administration.UserRow.localTextPrefix; };
            UserDialog.prototype.getNameProperty = function () { return Administration.UserRow.nameProperty; };
            UserDialog.prototype.getService = function () { return Administration.UserService.baseUrl; };
            UserDialog.prototype.getToolbarButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.push({
                    title: Q.text('Site.UserDialog.EditRolesButton'),
                    cssClass: 'edit-roles-button',
                    icon: 'icon-people text-blue',
                    onClick: function () {
                        new Administration.UserRoleDialog({
                            userID: _this.entity.UserId,
                            username: _this.entity.Username
                        }).dialogOpen();
                    }
                });
                // buttons.push({
                //     title: Q.text('Site.UserDialog.EditPermissionsButton'),
                //     cssClass: 'edit-permissions-button',
                //     icon: 'icon-lock-open text-green',
                //     onClick: () =>
                //     {
                //         new UserPermissionDialog({
                //             userID: this.entity.UserId,
                //             username: this.entity.Username
                //         }).dialogOpen();
                //     }
                // });
                return buttons;
            };
            UserDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                this.toolbar.findButton('edit-roles-button').toggleClass('disabled', this.isNewOrDeleted());
                //this.toolbar.findButton("edit-permissions-button").toggleClass("disabled", this.isNewOrDeleted());
                this.deleteButton.hide();
            };
            UserDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                // these fields are only required in new record mode
                this.form.Password.element.toggleClass('required', this.isNew())
                    .closest('.field').find('sup').toggle(this.isNew());
                this.form.PasswordConfirm.element.toggleClass('required', this.isNew())
                    .closest('.field').find('sup').toggle(this.isNew());
            };
            UserDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], UserDialog);
            return UserDialog;
        }(Serenity.EntityDialog));
        Administration.UserDialog = UserDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserGrid = (function (_super) {
            __extends(UserGrid, _super);
            function UserGrid(container) {
                _super.call(this, container);
            }
            UserGrid.prototype.getColumnsKey = function () { return "Administration.User"; };
            UserGrid.prototype.getDialogType = function () { return Administration.UserDialog; };
            UserGrid.prototype.getIdProperty = function () { return Administration.UserRow.idProperty; };
            UserGrid.prototype.getIsActiveProperty = function () { return Administration.UserRow.isActiveProperty; };
            UserGrid.prototype.getLocalTextPrefix = function () { return Administration.UserRow.localTextPrefix; };
            UserGrid.prototype.getService = function () { return Administration.UserService.baseUrl; };
            UserGrid.prototype.getDefaultSortBy = function () {
                return [Administration.UserRow.Fields.Username];
            };
            UserGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], UserGrid);
            return UserGrid;
        }(Serenity.EntityGrid));
        Administration.UserGrid = UserGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Authorization;
    (function (Authorization) {
        Object.defineProperty(Authorization, 'userDefinition', {
            get: function () {
                return Q.getRemoteData('UserData');
            }
        });
        function hasPermission(permissionKey) {
            var ud = Authorization.userDefinition;
            return ud.Username === 'admin' || !!ud.Permissions[permissionKey];
        }
        Authorization.hasPermission = hasPermission;
    })(Authorization = CaseManagement.Authorization || (CaseManagement.Authorization = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var PermissionCheckEditor = (function (_super) {
            __extends(PermissionCheckEditor, _super);
            function PermissionCheckEditor(container, opt) {
                var _this = this;
                _super.call(this, container, opt);
                this.rolePermissions = {};
                var titleByKey = {};
                var permissionKeys = this.getSortedGroupAndPermissionKeys(titleByKey);
                var items = permissionKeys.map(function (key) { return {
                    Key: key,
                    ParentKey: _this.getParentKey(key),
                    Title: titleByKey[key],
                    GrantRevoke: null,
                    IsGroup: key.charAt(key.length - 1) === ':'
                }; });
                this.byParentKey = Q.toGrouping(items, function (x) { return x.ParentKey; });
                this.setItems(items);
            }
            PermissionCheckEditor.prototype.getIdProperty = function () { return "Key"; };
            PermissionCheckEditor.prototype.getItemGrantRevokeClass = function (item, grant) {
                if (!item.IsGroup) {
                    return ((item.GrantRevoke === grant) ? ' checked' : '');
                }
                var desc = this.getDescendants(item, true);
                var granted = desc.filter(function (x) { return x.GrantRevoke === grant; });
                if (!granted.length) {
                    return '';
                }
                if (desc.length === granted.length) {
                    return 'checked';
                }
                return 'checked partial';
            };
            PermissionCheckEditor.prototype.getItemEffectiveClass = function (item) {
                var _this = this;
                if (item.IsGroup) {
                    var desc = this.getDescendants(item, true);
                    var grantCount = Q.count(desc, function (x) { return x.GrantRevoke === true ||
                        (x.GrantRevoke == null && _this.rolePermissions[x.Key]); });
                    if (grantCount === desc.length || desc.length === 0) {
                        return 'allow';
                    }
                    if (grantCount === 0) {
                        return 'deny';
                    }
                    return 'partial';
                }
                var granted = item.GrantRevoke === true ||
                    (item.GrantRevoke == null && this.rolePermissions[item.Key]);
                return (granted ? ' allow' : ' deny');
            };
            PermissionCheckEditor.prototype.getColumns = function () {
                var _this = this;
                var columns = [{
                        name: Q.text('Site.UserPermissionDialog.Permission'),
                        field: 'Title',
                        format: Serenity.SlickFormatting.treeToggle(function () { return _this.view; }, function (x) { return x.Key; }, function (ctx) {
                            var item = ctx.item;
                            var klass = _this.getItemEffectiveClass(item);
                            return '<span class="effective-permission ' + klass + '">' + Q.htmlEncode(ctx.value) + '</span>';
                        }),
                        width: 495,
                        sortable: false
                    }, {
                        name: Q.text('Site.UserPermissionDialog.Grant'), field: 'Grant',
                        format: function (ctx) {
                            var item1 = ctx.item;
                            var klass1 = _this.getItemGrantRevokeClass(item1, true);
                            return "<span class='check-box grant no-float " + klass1 + "'></span>";
                        },
                        width: 65,
                        sortable: false,
                        headerCssClass: 'align-center',
                        cssClass: 'align-center'
                    }];
                if (this.options.showRevoke) {
                    columns.push({
                        name: Q.text('Site.UserPermissionDialog.Revoke'), field: 'Revoke',
                        format: function (ctx) {
                            var item2 = ctx.item;
                            var klass2 = _this.getItemGrantRevokeClass(item2, false);
                            return '<span class="check-box revoke no-float ' + klass2 + '"></span>';
                        },
                        width: 65,
                        sortable: false,
                        headerCssClass: 'align-center',
                        cssClass: 'align-center'
                    });
                }
                return columns;
            };
            PermissionCheckEditor.prototype.setItems = function (items) {
                Serenity.SlickTreeHelper.setIndents(items, function (x) { return x.Key; }, function (x) { return x.ParentKey; }, false);
                this.view.setItems(items, true);
            };
            PermissionCheckEditor.prototype.onViewSubmit = function () {
                return false;
            };
            PermissionCheckEditor.prototype.onViewFilter = function (item) {
                var _this = this;
                if (!_super.prototype.onViewFilter.call(this, item)) {
                    return false;
                }
                if (!Serenity.SlickTreeHelper.filterById(item, this.view, function (x) { return x.ParentKey; }))
                    return false;
                if (this.searchText) {
                    return this.matchContains(item) || item.IsGroup && Q.any(this.getDescendants(item, false), function (x) { return _this.matchContains(x); });
                }
                return true;
            };
            PermissionCheckEditor.prototype.matchContains = function (item) {
                return Select2.util.stripDiacritics(item.Title || '').toLowerCase().indexOf(this.searchText) >= 0;
            };
            PermissionCheckEditor.prototype.getDescendants = function (item, excludeGroups) {
                var result = [];
                var stack = [item];
                while (stack.length > 0) {
                    var i = stack.pop();
                    var children = this.byParentKey[i.Key];
                    if (!children)
                        continue;
                    for (var _i = 0, children_1 = children; _i < children_1.length; _i++) {
                        var child = children_1[_i];
                        if (!excludeGroups || !child.IsGroup) {
                            result.push(child);
                        }
                        stack.push(child);
                    }
                }
                return result;
            };
            PermissionCheckEditor.prototype.onClick = function (e, row, cell) {
                _super.prototype.onClick.call(this, e, row, cell);
                if (!e.isDefaultPrevented()) {
                    Serenity.SlickTreeHelper.toggleClick(e, row, cell, this.view, function (x) { return x.Key; });
                }
                if (e.isDefaultPrevented()) {
                    return;
                }
                var target = $(e.target);
                var grant = target.hasClass('grant');
                if (grant || target.hasClass('revoke')) {
                    e.preventDefault();
                    var item = this.itemAt(row);
                    var checkedOrPartial = target.hasClass('checked') || target.hasClass('partial');
                    if (checkedOrPartial) {
                        grant = null;
                    }
                    else {
                        grant = grant !== checkedOrPartial;
                    }
                    if (item.IsGroup) {
                        for (var _i = 0, _a = this.getDescendants(item, true); _i < _a.length; _i++) {
                            var d = _a[_i];
                            d.GrantRevoke = grant;
                        }
                    }
                    else
                        item.GrantRevoke = grant;
                    this.slickGrid.invalidate();
                }
            };
            PermissionCheckEditor.prototype.getParentKey = function (key) {
                if (key.charAt(key.length - 1) === ':') {
                    key = key.substr(0, key.length - 1);
                }
                var idx = key.lastIndexOf(':');
                if (idx >= 0) {
                    return key.substr(0, idx + 1);
                }
                return null;
            };
            PermissionCheckEditor.prototype.getButtons = function () {
                return [];
            };
            PermissionCheckEditor.prototype.createToolbarExtensions = function () {
                var _this = this;
                _super.prototype.createToolbarExtensions.call(this);
                Serenity.GridUtils.addQuickSearchInputCustom(this.toolbar.element, function (field, text) {
                    _this.searchText = Select2.util.stripDiacritics(Q.trimToNull(text) || '').toLowerCase();
                    _this.view.setItems(_this.view.getItems(), true);
                });
            };
            PermissionCheckEditor.prototype.getSortedGroupAndPermissionKeys = function (titleByKey) {
                var keys = Q.getRemoteData('Administration.PermissionKeys').Entities;
                var titleWithGroup = {};
                for (var _i = 0, keys_1 = keys; _i < keys_1.length; _i++) {
                    var k = keys_1[_i];
                    var s = k;
                    if (!s) {
                        continue;
                    }
                    if (s.charAt(s.length - 1) == ':') {
                        s = s.substr(0, s.length - 1);
                        if (s.length === 0) {
                            continue;
                        }
                    }
                    if (titleByKey[s]) {
                        continue;
                    }
                    titleByKey[s] = Q.coalesce(Q.tryGetText('Permission.' + s), s);
                    var parts = s.split(':');
                    var group = '';
                    var groupTitle = '';
                    for (var i = 0; i < parts.length - 1; i++) {
                        group = group + parts[i] + ':';
                        var txt = Q.tryGetText('Permission.' + group);
                        if (txt == null) {
                            txt = parts[i];
                        }
                        titleByKey[group] = txt;
                        groupTitle = groupTitle + titleByKey[group] + ':';
                        titleWithGroup[group] = groupTitle;
                    }
                    titleWithGroup[s] = groupTitle + titleByKey[s];
                }
                keys = Object.keys(titleByKey);
                keys = keys.sort(function (x, y) { return Q.turkishLocaleCompare(titleWithGroup[x], titleWithGroup[y]); });
                return keys;
            };
            PermissionCheckEditor.prototype.get_value = function () {
                var result = [];
                for (var _i = 0, _a = this.view.getItems(); _i < _a.length; _i++) {
                    var item = _a[_i];
                    if (item.GrantRevoke != null && item.Key.charAt(item.Key.length - 1) != ':') {
                        result.push({ PermissionKey: item.Key, Granted: item.GrantRevoke });
                    }
                }
                return result;
            };
            PermissionCheckEditor.prototype.set_value = function (value) {
                for (var _i = 0, _a = this.view.getItems(); _i < _a.length; _i++) {
                    var item = _a[_i];
                    item.GrantRevoke = null;
                }
                if (value != null) {
                    for (var _b = 0, value_1 = value; _b < value_1.length; _b++) {
                        var row = value_1[_b];
                        var r = this.view.getItemById(row.PermissionKey);
                        if (r) {
                            r.GrantRevoke = Q.coalesce(row.Granted, true);
                        }
                    }
                }
                this.setItems(this.getItems());
            };
            PermissionCheckEditor.prototype.get_rolePermissions = function () {
                return Object.keys(this.rolePermissions);
            };
            PermissionCheckEditor.prototype.set_rolePermissions = function (value) {
                this.rolePermissions = {};
                if (value) {
                    for (var _i = 0, value_2 = value; _i < value_2.length; _i++) {
                        var k = value_2[_i];
                        this.rolePermissions[k] = true;
                    }
                }
                this.setItems(this.getItems());
            };
            PermissionCheckEditor = __decorate([
                Serenity.Decorators.registerEditor([Serenity.IGetEditValue, Serenity.ISetEditValue])
            ], PermissionCheckEditor);
            return PermissionCheckEditor;
        }(Serenity.DataGrid));
        Administration.PermissionCheckEditor = PermissionCheckEditor;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserPermissionDialog = (function (_super) {
            __extends(UserPermissionDialog, _super);
            function UserPermissionDialog(opt) {
                var _this = this;
                _super.call(this, opt);
                this.permissions = new Administration.PermissionCheckEditor(this.byId('Permissions'), {
                    showRevoke: true
                });
                Administration.UserPermissionService.List({
                    UserID: this.options.userID,
                    Module: null,
                    Submodule: null
                }, function (response) {
                    _this.permissions.set_value(response.Entities);
                });
                Administration.UserPermissionService.ListRolePermissions({
                    UserID: this.options.userID,
                    Module: null,
                    Submodule: null,
                }, function (response) {
                    _this.permissions.set_rolePermissions(response.Entities);
                });
            }
            UserPermissionDialog.prototype.getDialogOptions = function () {
                var _this = this;
                var opt = _super.prototype.getDialogOptions.call(this);
                opt.buttons = [
                    {
                        text: Q.text('Dialogs.OkButton'),
                        click: function (e) {
                            Administration.UserPermissionService.Update({
                                UserID: _this.options.userID,
                                Permissions: _this.permissions.get_value(),
                                Module: null,
                                Submodule: null
                            }, function (response) {
                                _this.dialogClose();
                                window.setTimeout(function () { return Q.notifySuccess(Q.text('Site.UserPermissionDialog.SaveSuccess')); }, 0);
                            });
                        }
                    }, {
                        text: Q.text('Dialogs.CancelButton'),
                        click: function () { return _this.dialogClose(); }
                    }];
                opt.title = Q.format(Q.text('Site.UserPermissionDialog.DialogTitle'), this.options.username);
                return opt;
            };
            UserPermissionDialog.prototype.getTemplate = function () {
                return '<div id="~_Permissions"></div>';
            };
            UserPermissionDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], UserPermissionDialog);
            return UserPermissionDialog;
        }(Serenity.TemplatedDialog));
        Administration.UserPermissionDialog = UserPermissionDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleCheckEditor = (function (_super) {
            __extends(RoleCheckEditor, _super);
            function RoleCheckEditor(div) {
                _super.call(this, div);
            }
            RoleCheckEditor.prototype.createToolbarExtensions = function () {
                var _this = this;
                _super.prototype.createToolbarExtensions.call(this);
                Serenity.GridUtils.addQuickSearchInputCustom(this.toolbar.element, function (field, text) {
                    _this.searchText = Select2.util.stripDiacritics(text || '').toUpperCase();
                    _this.view.setItems(_this.view.getItems(), true);
                });
            };
            RoleCheckEditor.prototype.getButtons = function () {
                return [];
            };
            RoleCheckEditor.prototype.getTreeItems = function () {
                return Administration.RoleRow.getLookup().items.map(function (role) { return {
                    id: role.RoleId.toString(),
                    text: role.RoleName
                }; });
            };
            RoleCheckEditor.prototype.onViewFilter = function (item) {
                return _super.prototype.onViewFilter.call(this, item) &&
                    (Q.isEmptyOrNull(this.searchText) ||
                        Select2.util.stripDiacritics(item.text || '')
                            .toUpperCase().indexOf(this.searchText) >= 0);
            };
            RoleCheckEditor = __decorate([
                Serenity.Decorators.registerEditor()
            ], RoleCheckEditor);
            return RoleCheckEditor;
        }(Serenity.CheckTreeEditor));
        Administration.RoleCheckEditor = RoleCheckEditor;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserRoleDialog = (function (_super) {
            __extends(UserRoleDialog, _super);
            function UserRoleDialog(opt) {
                var _this = this;
                _super.call(this, opt);
                this.permissions = new Administration.RoleCheckEditor(this.byId('Roles'));
                Administration.UserRoleService.List({
                    UserID: this.options.userID
                }, function (response) {
                    _this.permissions.value = response.Entities.map(function (x) { return x.toString(); });
                });
            }
            UserRoleDialog.prototype.getDialogOptions = function () {
                var _this = this;
                var opt = _super.prototype.getDialogOptions.call(this);
                opt.buttons = [{
                        text: Q.text('Dialogs.OkButton'),
                        click: function () {
                            Q.serviceRequest('Administration/UserRole/Update', {
                                UserID: _this.options.userID,
                                Roles: _this.permissions.value.map(function (x) { return parseInt(x, 10); })
                            }, function (response) {
                                _this.dialogClose();
                                Q.notifySuccess(Q.text('Site.UserRoleDialog.SaveSuccess'));
                            });
                        }
                    }, {
                        text: Q.text('Dialogs.CancelButton'),
                        click: function () { return _this.dialogClose(); }
                    }];
                opt.title = Q.format(Q.text('Site.UserRoleDialog.DialogTitle'), this.options.username);
                return opt;
            };
            UserRoleDialog.prototype.getTemplate = function () {
                return "<div id='~_Roles'></div>";
            };
            UserRoleDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], UserRoleDialog);
            return UserRoleDialog;
        }(Serenity.TemplatedDialog));
        Administration.UserRoleDialog = UserRoleDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserSupportGroupDialog = (function (_super) {
            __extends(UserSupportGroupDialog, _super);
            function UserSupportGroupDialog() {
                _super.apply(this, arguments);
                this.form = new Administration.UserSupportGroupForm(this.idPrefix);
            }
            UserSupportGroupDialog.prototype.getFormKey = function () { return Administration.UserSupportGroupForm.formKey; };
            UserSupportGroupDialog.prototype.getIdProperty = function () { return Administration.UserSupportGroupRow.idProperty; };
            UserSupportGroupDialog.prototype.getLocalTextPrefix = function () { return Administration.UserSupportGroupRow.localTextPrefix; };
            UserSupportGroupDialog.prototype.getService = function () { return Administration.UserSupportGroupService.baseUrl; };
            UserSupportGroupDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], UserSupportGroupDialog);
            return UserSupportGroupDialog;
        }(Serenity.EntityDialog));
        Administration.UserSupportGroupDialog = UserSupportGroupDialog;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserSupportGroupGrid = (function (_super) {
            __extends(UserSupportGroupGrid, _super);
            function UserSupportGroupGrid(container) {
                _super.call(this, container);
            }
            UserSupportGroupGrid.prototype.getColumnsKey = function () { return 'Administration.UserSupportGroup'; };
            UserSupportGroupGrid.prototype.getDialogType = function () { return Administration.UserSupportGroupDialog; };
            UserSupportGroupGrid.prototype.getIdProperty = function () { return Administration.UserSupportGroupRow.idProperty; };
            UserSupportGroupGrid.prototype.getLocalTextPrefix = function () { return Administration.UserSupportGroupRow.localTextPrefix; };
            UserSupportGroupGrid.prototype.getService = function () { return Administration.UserSupportGroupService.baseUrl; };
            UserSupportGroupGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], UserSupportGroupGrid);
            return UserSupportGroupGrid;
        }(Serenity.EntityGrid));
        Administration.UserSupportGroupGrid = UserSupportGroupGrid;
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityDialog = (function (_super) {
            __extends(ActivityDialog, _super);
            function ActivityDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityForm(this.idPrefix);
            }
            ActivityDialog.prototype.getFormKey = function () { return Case.ActivityForm.formKey; };
            ActivityDialog.prototype.getIdProperty = function () { return Case.ActivityRow.idProperty; };
            ActivityDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRow.localTextPrefix; };
            ActivityDialog.prototype.getNameProperty = function () { return Case.ActivityRow.nameProperty; };
            ActivityDialog.prototype.getService = function () { return Case.ActivityService.baseUrl; };
            ActivityDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityDialog);
            return ActivityDialog;
        }(Serenity.EntityDialog));
        Case.ActivityDialog = ActivityDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGrid = (function (_super) {
            __extends(ActivityGrid, _super);
            function ActivityGrid(container) {
                _super.call(this, container);
            }
            ActivityGrid.prototype.getColumnsKey = function () { return 'Case.Activity'; };
            ActivityGrid.prototype.getDialogType = function () { return Case.ActivityDialog; };
            ActivityGrid.prototype.getIdProperty = function () { return Case.ActivityRow.idProperty; };
            ActivityGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRow.localTextPrefix; };
            ActivityGrid.prototype.getService = function () { return Case.ActivityService.baseUrl; };
            ActivityGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityGrid);
            return ActivityGrid;
        }(Serenity.EntityGrid));
        Case.ActivityGrid = ActivityGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationDialog = (function (_super) {
            __extends(ActivityCorrectionOperationDialog, _super);
            function ActivityCorrectionOperationDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityCorrectionOperationForm(this.idPrefix);
            }
            ActivityCorrectionOperationDialog.prototype.getFormKey = function () { return Case.ActivityCorrectionOperationForm.formKey; };
            ActivityCorrectionOperationDialog.prototype.getIdProperty = function () { return Case.ActivityCorrectionOperationRow.idProperty; };
            ActivityCorrectionOperationDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityCorrectionOperationRow.localTextPrefix; };
            ActivityCorrectionOperationDialog.prototype.getNameProperty = function () { return Case.ActivityCorrectionOperationRow.nameProperty; };
            ActivityCorrectionOperationDialog.prototype.getService = function () { return Case.ActivityCorrectionOperationService.baseUrl; };
            ActivityCorrectionOperationDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityCorrectionOperationDialog);
            return ActivityCorrectionOperationDialog;
        }(Serenity.EntityDialog));
        Case.ActivityCorrectionOperationDialog = ActivityCorrectionOperationDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var GridEditorDialog = (function (_super) {
            __extends(GridEditorDialog, _super);
            function GridEditorDialog() {
                _super.apply(this, arguments);
            }
            GridEditorDialog.prototype.getIdProperty = function () { return "__id"; };
            GridEditorDialog.prototype.destroy = function () {
                this.onSave = null;
                this.onDelete = null;
                _super.prototype.destroy.call(this);
            };
            GridEditorDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                // apply changes button doesn't work properly with in-memory grids yet
                if (this.applyChangesButton) {
                    this.applyChangesButton.hide();
                }
            };
            GridEditorDialog.prototype.saveHandler = function (options, callback) {
                this.onSave && this.onSave(options, callback);
            };
            GridEditorDialog.prototype.deleteHandler = function (options, callback) {
                this.onDelete && this.onDelete(options, callback);
            };
            GridEditorDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], GridEditorDialog);
            return GridEditorDialog;
        }(Serenity.EntityDialog));
        Common.GridEditorDialog = GridEditorDialog;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationEditDialog = (function (_super) {
            __extends(ActivityCorrectionOperationEditDialog, _super);
            function ActivityCorrectionOperationEditDialog() {
                _super.call(this);
                this.form = new Case.ActivityCorrectionOperationForm(this.idPrefix);
            }
            ActivityCorrectionOperationEditDialog.prototype.getFormKey = function () { return Case.ActivityCorrectionOperationForm.formKey; };
            ActivityCorrectionOperationEditDialog.prototype.getNameProperty = function () { return Case.ActivityCorrectionOperationRow.nameProperty; };
            ActivityCorrectionOperationEditDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityCorrectionOperationRow.localTextPrefix; };
            ActivityCorrectionOperationEditDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityCorrectionOperationEditDialog);
            return ActivityCorrectionOperationEditDialog;
        }(CaseManagement.Common.GridEditorDialog));
        Case.ActivityCorrectionOperationEditDialog = ActivityCorrectionOperationEditDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var GridEditorBase = (function (_super) {
            __extends(GridEditorBase, _super);
            function GridEditorBase(container) {
                _super.call(this, container);
                this.nextId = 1;
            }
            GridEditorBase.prototype.getIdProperty = function () { return "__id"; };
            GridEditorBase.prototype.id = function (entity) {
                return entity.__id;
            };
            GridEditorBase.prototype.save = function (opt, callback) {
                var _this = this;
                var request = opt.request;
                var row = Q.deepClone(request.Entity);
                var id = row.__id;
                if (id == null) {
                    row.__id = this.nextId++;
                }
                if (!this.validateEntity(row, id)) {
                    return;
                }
                var items = this.view.getItems().slice();
                if (id == null) {
                    items.push(row);
                }
                else {
                    var index = Q.indexOf(items, function (x) { return _this.id(x) === id; });
                    items[index] = Q.deepClone({}, items[index], row);
                }
                this.setEntities(items);
                callback({});
            };
            GridEditorBase.prototype.deleteEntity = function (id) {
                this.view.deleteItem(id);
                return true;
            };
            GridEditorBase.prototype.validateEntity = function (row, id) {
                return true;
            };
            GridEditorBase.prototype.setEntities = function (items) {
                this.view.setItems(items, true);
            };
            GridEditorBase.prototype.getNewEntity = function () {
                return {};
            };
            GridEditorBase.prototype.getButtons = function () {
                var _this = this;
                return [{
                        title: this.getAddButtonCaption(),
                        cssClass: 'add-button',
                        onClick: function () {
                            _this.createEntityDialog(_this.getItemType(), function (dlg) {
                                var dialog = dlg;
                                dialog.onSave = function (opt, callback) { return _this.save(opt, callback); };
                                dialog.loadEntityAndOpenDialog(_this.getNewEntity());
                            });
                        }
                    }];
            };
            GridEditorBase.prototype.editItem = function (entityOrId) {
                var _this = this;
                var id = entityOrId;
                var item = this.view.getItemById(id);
                this.createEntityDialog(this.getItemType(), function (dlg) {
                    var dialog = dlg;
                    dialog.onDelete = function (opt, callback) {
                        if (!_this.deleteEntity(id)) {
                            return;
                        }
                        callback({});
                    };
                    dialog.onSave = function (opt, callback) { return _this.save(opt, callback); };
                    dialog.loadEntityAndOpenDialog(item);
                });
                ;
            };
            GridEditorBase.prototype.getEditValue = function (property, target) {
                target[property.name] = this.value;
            };
            GridEditorBase.prototype.setEditValue = function (source, property) {
                this.value = source[property.name];
            };
            Object.defineProperty(GridEditorBase.prototype, "value", {
                get: function () {
                    return this.view.getItems().map(function (x) {
                        var y = Q.deepClone(x);
                        delete y['__id'];
                        return y;
                    });
                },
                set: function (value) {
                    var _this = this;
                    this.view.setItems((value || []).map(function (x) {
                        var y = Q.deepClone(x);
                        y.__id = _this.nextId++;
                        return y;
                    }), true);
                },
                enumerable: true,
                configurable: true
            });
            GridEditorBase.prototype.getGridCanLoad = function () {
                return false;
            };
            GridEditorBase.prototype.usePager = function () {
                return false;
            };
            GridEditorBase.prototype.getInitialTitle = function () {
                return null;
            };
            GridEditorBase.prototype.createQuickSearchInput = function () {
            };
            GridEditorBase = __decorate([
                Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue]),
                Serenity.Decorators.editor(),
                Serenity.Decorators.element("<div/>")
            ], GridEditorBase);
            return GridEditorBase;
        }(Serenity.EntityGrid));
        Common.GridEditorBase = GridEditorBase;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationEditor = (function (_super) {
            __extends(ActivityCorrectionOperationEditor, _super);
            function ActivityCorrectionOperationEditor(container) {
                _super.call(this, container);
            }
            ActivityCorrectionOperationEditor.prototype.getColumnsKey = function () { return "Case.ActivityCorrectionOperation"; };
            ActivityCorrectionOperationEditor.prototype.getDialogType = function () { return Case.ActivityCorrectionOperationEditDialog; };
            ActivityCorrectionOperationEditor.prototype.getLocalTextPrefix = function () { return Case.ActivityCorrectionOperationRow.localTextPrefix; };
            ActivityCorrectionOperationEditor.prototype.getAddButtonCaption = function () {
                return "افزودن";
            };
            ActivityCorrectionOperationEditor = __decorate([
                Serenity.Decorators.registerEditor()
            ], ActivityCorrectionOperationEditor);
            return ActivityCorrectionOperationEditor;
        }(CaseManagement.Common.GridEditorBase));
        Case.ActivityCorrectionOperationEditor = ActivityCorrectionOperationEditor;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGroupDialog = (function (_super) {
            __extends(ActivityGroupDialog, _super);
            function ActivityGroupDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityGroupForm(this.idPrefix);
            }
            ActivityGroupDialog.prototype.getFormKey = function () { return Case.ActivityGroupForm.formKey; };
            ActivityGroupDialog.prototype.getIdProperty = function () { return Case.ActivityGroupRow.idProperty; };
            ActivityGroupDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityGroupRow.localTextPrefix; };
            ActivityGroupDialog.prototype.getNameProperty = function () { return Case.ActivityGroupRow.nameProperty; };
            ActivityGroupDialog.prototype.getService = function () { return Case.ActivityGroupService.baseUrl; };
            ActivityGroupDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityGroupDialog);
            return ActivityGroupDialog;
        }(Serenity.EntityDialog));
        Case.ActivityGroupDialog = ActivityGroupDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGroupGrid = (function (_super) {
            __extends(ActivityGroupGrid, _super);
            function ActivityGroupGrid(container) {
                _super.call(this, container);
            }
            ActivityGroupGrid.prototype.getColumnsKey = function () { return 'Case.ActivityGroup'; };
            ActivityGroupGrid.prototype.getDialogType = function () { return Case.ActivityGroupDialog; };
            ActivityGroupGrid.prototype.getIdProperty = function () { return Case.ActivityGroupRow.idProperty; };
            ActivityGroupGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityGroupRow.localTextPrefix; };
            ActivityGroupGrid.prototype.getService = function () { return Case.ActivityGroupService.baseUrl; };
            ActivityGroupGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityGroupService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                return buttons;
            };
            ActivityGroupGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityGroupGrid);
            return ActivityGroupGrid;
        }(Serenity.EntityGrid));
        Case.ActivityGroupGrid = ActivityGroupGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityHelpDialog = (function (_super) {
            __extends(ActivityHelpDialog, _super);
            function ActivityHelpDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityHelpForm(this.idPrefix);
            }
            ActivityHelpDialog.prototype.getFormKey = function () { return Case.ActivityHelpForm.formKey; };
            ActivityHelpDialog.prototype.getIdProperty = function () { return Case.ActivityHelpRow.idProperty; };
            ActivityHelpDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityHelpRow.localTextPrefix; };
            ActivityHelpDialog.prototype.getNameProperty = function () { return Case.ActivityHelpRow.nameProperty; };
            ActivityHelpDialog.prototype.getService = function () { return Case.ActivityHelpService.baseUrl; };
            ActivityHelpDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            ActivityHelpDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            ActivityHelpDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityHelpDialog);
            return ActivityHelpDialog;
        }(Serenity.EntityDialog));
        Case.ActivityHelpDialog = ActivityHelpDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityHelpGrid = (function (_super) {
            __extends(ActivityHelpGrid, _super);
            function ActivityHelpGrid(container) {
                _super.call(this, container);
            }
            ActivityHelpGrid.prototype.getColumnsKey = function () { return 'Case.ActivityHelp'; };
            ActivityHelpGrid.prototype.getDialogType = function () { return Case.ActivityHelpDialog; };
            ActivityHelpGrid.prototype.getIdProperty = function () { return Case.ActivityHelpRow.idProperty; };
            ActivityHelpGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityHelpRow.localTextPrefix; };
            ActivityHelpGrid.prototype.getService = function () { return Case.ActivityHelpService.baseUrl; };
            ActivityHelpGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityHelpGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityHelpGrid);
            return ActivityHelpGrid;
        }(Serenity.EntityGrid));
        Case.ActivityHelpGrid = ActivityHelpGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonDialog = (function (_super) {
            __extends(ActivityMainReasonDialog, _super);
            function ActivityMainReasonDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityMainReasonForm(this.idPrefix);
            }
            ActivityMainReasonDialog.prototype.getFormKey = function () { return Case.ActivityMainReasonForm.formKey; };
            ActivityMainReasonDialog.prototype.getIdProperty = function () { return Case.ActivityMainReasonRow.idProperty; };
            ActivityMainReasonDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityMainReasonRow.localTextPrefix; };
            ActivityMainReasonDialog.prototype.getNameProperty = function () { return Case.ActivityMainReasonRow.nameProperty; };
            ActivityMainReasonDialog.prototype.getService = function () { return Case.ActivityMainReasonService.baseUrl; };
            ActivityMainReasonDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityMainReasonDialog);
            return ActivityMainReasonDialog;
        }(Serenity.EntityDialog));
        Case.ActivityMainReasonDialog = ActivityMainReasonDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonEditDialog = (function (_super) {
            __extends(ActivityMainReasonEditDialog, _super);
            function ActivityMainReasonEditDialog() {
                _super.call(this);
                this.form = new Case.ActivityMainReasonForm(this.idPrefix);
            }
            ActivityMainReasonEditDialog.prototype.getFormKey = function () { return Case.ActivityMainReasonForm.formKey; };
            ActivityMainReasonEditDialog.prototype.getNameProperty = function () { return Case.ActivityMainReasonRow.nameProperty; };
            ActivityMainReasonEditDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityMainReasonRow.localTextPrefix; };
            ActivityMainReasonEditDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityMainReasonEditDialog);
            return ActivityMainReasonEditDialog;
        }(CaseManagement.Common.GridEditorDialog));
        Case.ActivityMainReasonEditDialog = ActivityMainReasonEditDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonEditor = (function (_super) {
            __extends(ActivityMainReasonEditor, _super);
            function ActivityMainReasonEditor(container) {
                _super.call(this, container);
            }
            ActivityMainReasonEditor.prototype.getColumnsKey = function () { return "Case.ActivityMainReason"; };
            ActivityMainReasonEditor.prototype.getDialogType = function () { return Case.ActivityMainReasonEditDialog; };
            ActivityMainReasonEditor.prototype.getLocalTextPrefix = function () { return Case.ActivityMainReasonRow.localTextPrefix; };
            ActivityMainReasonEditor.prototype.getAddButtonCaption = function () {
                return "افزودن";
            };
            ActivityMainReasonEditor = __decorate([
                Serenity.Decorators.registerEditor()
            ], ActivityMainReasonEditor);
            return ActivityMainReasonEditor;
        }(CaseManagement.Common.GridEditorBase));
        Case.ActivityMainReasonEditor = ActivityMainReasonEditor;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDialog = (function (_super) {
            __extends(ActivityRequestDialog, _super);
            function ActivityRequestDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.form = new Case.ActivityRequestForm(this.idPrefix);
                this.form.CycleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var factor = Q.toId(_this.form.Factor.value);
                    if (cycle != null && factor != null) {
                        var year = cycle * factor;
                        _this.form.YearCost.value = year.toString();
                    }
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                    var accessible = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessible != null) {
                        var recoverableLeakage = cycle + accessible;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.DelayedCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                });
                this.form.AccessibleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var accessibleCost = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessibleCost != null) {
                        var recoverableLeakage = cycle + accessibleCost;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.Factor.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var factor = Q.toId(_this.form.Factor.value);
                    if (cycle != null && factor != null) {
                        var year = cycle * factor;
                        _this.form.YearCost.value = year.toString();
                    }
                });
                this.form.ActivityId.changeSelect2(function (e) {
                    var activityID = Q.toId(_this.form.ActivityId.value);
                    if (activityID != null) {
                        _this.form.EventDescription.value = Case.ActivityRow.getLookup().itemById[activityID].EventDescription;
                    }
                });
            }
            ActivityRequestDialog.prototype.getFormKey = function () { return Case.ActivityRequestForm.formKey; };
            ActivityRequestDialog.prototype.getIdProperty = function () { return Case.ActivityRequestRow.idProperty; };
            ActivityRequestDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestRow.localTextPrefix; };
            ActivityRequestDialog.prototype.getService = function () { return Case.ActivityRequestService.baseUrl; };
            ActivityRequestDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive(),
                Serenity.Decorators.maximizable()
            ], ActivityRequestDialog);
            return ActivityRequestDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestDialog = ActivityRequestDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestGrid = (function (_super) {
            __extends(ActivityRequestGrid, _super);
            function ActivityRequestGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequest'; };
            ActivityRequestGrid.prototype.getDialogType = function () { return Case.ActivityRequestDialog; };
            ActivityRequestGrid.prototype.getIdProperty = function () { return Case.ActivityRequestRow.idProperty; };
            ActivityRequestGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestRow.localTextPrefix; };
            ActivityRequestGrid.prototype.getService = function () { return Case.ActivityRequestService.baseUrl; };
            ActivityRequestGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                return buttons;
            };
            ActivityRequestGrid.prototype.getItemCssClass = function (item, index) {
                var klass = "";
                // if (item.IsRejected == true) 
                //     klass += " actionReject";            
                if (item.ConfirmTypeID == 2)
                    klass += " financialConfirm";
                return Q.trimToNull(klass);
            };
            ActivityRequestGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestGrid);
            return ActivityRequestGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestGrid = ActivityRequestGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLogGrid = (function (_super) {
            __extends(ActivityRequestLogGrid, _super);
            function ActivityRequestLogGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestLogGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestLog'; };
            //protected getDialogType() { return ActivityRequestLogDialog; }
            ActivityRequestLogGrid.prototype.getIdProperty = function () { return Case.ActivityRequestLogRow.idProperty; };
            ActivityRequestLogGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestLogRow.localTextPrefix; };
            ActivityRequestLogGrid.prototype.getService = function () { return Case.ActivityRequestLogService.baseUrl; };
            ActivityRequestLogGrid.prototype.getButtons = function () {
                return null;
            };
            ActivityRequestLogGrid.prototype.getInitialTitle = function () {
                return null;
            };
            ActivityRequestLogGrid.prototype.usePager = function () {
                return false;
            };
            ActivityRequestLogGrid.prototype.getGridCanLoad = function () {
                return this.ActivityRequestID != null;
            };
            Object.defineProperty(ActivityRequestLogGrid.prototype, "ActivityRequestID", {
                get: function () {
                    return this._ActivityRequestID;
                },
                set: function (value) {
                    if (this._ActivityRequestID != value) {
                        this._ActivityRequestID = value;
                        this.setEquality(Case.ActivityRequestLogRow.Fields.ActivityRequestId, value);
                        this.refresh();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ActivityRequestLogGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestLogGrid);
            return ActivityRequestLogGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestLogGrid = ActivityRequestLogGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentEditDialog = (function (_super) {
            __extends(ActivityRequestCommentEditDialog, _super);
            function ActivityRequestCommentEditDialog() {
                _super.call(this);
                this.form = new Case.ActivityRequestCommentForm(this.idPrefix);
            }
            ActivityRequestCommentEditDialog.prototype.getFormKey = function () { return Case.ActivityRequestCommentForm.formKey; };
            ActivityRequestCommentEditDialog.prototype.getNameProperty = function () { return Case.ActivityRequestCommentRow.nameProperty; };
            ActivityRequestCommentEditDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestCommentRow.localTextPrefix; };
            ActivityRequestCommentEditDialog = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestCommentEditDialog);
            return ActivityRequestCommentEditDialog;
        }(CaseManagement.Common.GridEditorDialog));
        Case.ActivityRequestCommentEditDialog = ActivityRequestCommentEditDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentEditor = (function (_super) {
            __extends(ActivityRequestCommentEditor, _super);
            function ActivityRequestCommentEditor(container) {
                _super.call(this, container);
            }
            ActivityRequestCommentEditor.prototype.getColumnsKey = function () { return "Case.ActivityRequestComment"; };
            ActivityRequestCommentEditor.prototype.getDialogType = function () { return Case.ActivityRequestCommentEditDialog; };
            ActivityRequestCommentEditor.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestCommentRow.localTextPrefix; };
            ActivityRequestCommentEditor.prototype.getAddButtonCaption = function () {
                return "افزودن";
            };
            ActivityRequestCommentEditor = __decorate([
                Serenity.Decorators.registerEditor()
            ], ActivityRequestCommentEditor);
            return ActivityRequestCommentEditor;
        }(CaseManagement.Common.GridEditorBase));
        Case.ActivityRequestCommentEditor = ActivityRequestCommentEditor;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentReasonDialog = (function (_super) {
            __extends(ActivityRequestCommentReasonDialog, _super);
            function ActivityRequestCommentReasonDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityRequestCommentReasonForm(this.idPrefix);
            }
            ActivityRequestCommentReasonDialog.prototype.getFormKey = function () { return Case.ActivityRequestCommentReasonForm.formKey; };
            ActivityRequestCommentReasonDialog.prototype.getIdProperty = function () { return Case.ActivityRequestCommentReasonRow.idProperty; };
            ActivityRequestCommentReasonDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestCommentReasonRow.localTextPrefix; };
            ActivityRequestCommentReasonDialog.prototype.getService = function () { return Case.ActivityRequestCommentReasonService.baseUrl; };
            ActivityRequestCommentReasonDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestCommentReasonDialog);
            return ActivityRequestCommentReasonDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestCommentReasonDialog = ActivityRequestCommentReasonDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentReasonGrid = (function (_super) {
            __extends(ActivityRequestCommentReasonGrid, _super);
            function ActivityRequestCommentReasonGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestCommentReasonGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestCommentReason'; };
            ActivityRequestCommentReasonGrid.prototype.getDialogType = function () { return Case.ActivityRequestCommentReasonDialog; };
            ActivityRequestCommentReasonGrid.prototype.getIdProperty = function () { return Case.ActivityRequestCommentReasonRow.idProperty; };
            ActivityRequestCommentReasonGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestCommentReasonRow.localTextPrefix; };
            ActivityRequestCommentReasonGrid.prototype.getService = function () { return Case.ActivityRequestCommentReasonService.baseUrl; };
            ActivityRequestCommentReasonGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestCommentReasonGrid);
            return ActivityRequestCommentReasonGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestCommentReasonGrid = ActivityRequestCommentReasonGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmDialog = (function (_super) {
            __extends(ActivityRequestConfirmDialog, _super);
            function ActivityRequestConfirmDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestConfirmForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ActivityRequestConfirmDialog.prototype.getFormKey = function () { return Case.ActivityRequestConfirmForm.formKey; };
            ActivityRequestConfirmDialog.prototype.getIdProperty = function () { return Case.ActivityRequestConfirmRow.idProperty; };
            ActivityRequestConfirmDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestConfirmRow.localTextPrefix; };
            ActivityRequestConfirmDialog.prototype.getService = function () { return Case.ActivityRequestConfirmService.baseUrl; };
            ActivityRequestConfirmDialog.prototype.getToolbarButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.push({
                    title: Q.text('چاپ فعالیت'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var activityID = _this.form.Id.value;
                        window.location.href = "../Common/ActivityRequestConfirmedInfoTOPrint?ActivityId=" + activityID;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            ActivityRequestConfirmDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            ActivityRequestConfirmDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestConfirmDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestConfirmDialog);
            return ActivityRequestConfirmDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestConfirmDialog = ActivityRequestConfirmDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmGrid = (function (_super) {
            __extends(ActivityRequestConfirmGrid, _super);
            function ActivityRequestConfirmGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestConfirmGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestConfirm'; };
            ActivityRequestConfirmGrid.prototype.getDialogType = function () { return Case.ActivityRequestConfirmDialog; };
            ActivityRequestConfirmGrid.prototype.getIdProperty = function () { return Case.ActivityRequestConfirmRow.idProperty; };
            ActivityRequestConfirmGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestConfirmRow.localTextPrefix; };
            ActivityRequestConfirmGrid.prototype.getService = function () { return Case.ActivityRequestConfirmService.baseUrl; };
            ActivityRequestConfirmGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('CycleCost'),
                        new Slick.Aggregators.Sum('DelayedCost'),
                        new Slick.Aggregators.Sum('TotalLeakage'),
                        new Slick.Aggregators.Sum('RecoverableLeakage'),
                        new Slick.Aggregators.Sum('Recovered')
                    ]
                });
                return grid;
            };
            ActivityRequestConfirmGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestConfirmGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestConfirmService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var CreateTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(CreateTime_Start);
                        var CreateTime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(CreateTime_End);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        //var EndTime_Start = AllFilters[5].getElementsByTagName('input')[0].value; //console.log(EndTime_Start);
                        //var EndTime_End = AllFilters[5].getElementsByTagName('input')[1].value; //console.log(AllFilters[5].getElementsByTagName('input')[1].innerHTML);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        }
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        }
                        window.location.href = "../Common/ActivityRequestConfirmPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&CreateTime_Start=" + CreateTime_Start + "&CreateTime_End=" + CreateTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestConfirmGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestConfirmGrid);
            return ActivityRequestConfirmGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestConfirmGrid = ActivityRequestConfirmGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmAdminDialog = (function (_super) {
            __extends(ActivityRequestConfirmAdminDialog, _super);
            function ActivityRequestConfirmAdminDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestConfirmAdminForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ActivityRequestConfirmAdminDialog.prototype.getFormKey = function () { return Case.ActivityRequestConfirmAdminForm.formKey; };
            ActivityRequestConfirmAdminDialog.prototype.getIdProperty = function () { return Case.ActivityRequestConfirmAdminRow.idProperty; };
            ActivityRequestConfirmAdminDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestConfirmAdminRow.localTextPrefix; };
            ActivityRequestConfirmAdminDialog.prototype.getService = function () { return Case.ActivityRequestConfirmAdminService.baseUrl; };
            ActivityRequestConfirmAdminDialog.prototype.getToolbarButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getToolbarButtons.call(this);
                //buttons.splice(Q.indexOf(buttons, x => x.cssClass == "save-and-close-button"), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var activityID = _this.form.Id.value;
                        window.location.href = "../Common/ActivityRequestTechnicalInfoTOPrint?ActivityId=" + activityID;
                    }
                });
                return buttons;
            };
            //  protected updateInterface(): void {
            //
            //      super.updateInterface();
            //
            //      Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
            //      this.element.find('sup').hide();
            //      this.deleteButton.hide();
            //  }
            ActivityRequestConfirmAdminDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestConfirmAdminDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestConfirmAdminDialog);
            return ActivityRequestConfirmAdminDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestConfirmAdminDialog = ActivityRequestConfirmAdminDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmAdminGrid = (function (_super) {
            __extends(ActivityRequestConfirmAdminGrid, _super);
            function ActivityRequestConfirmAdminGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestConfirmAdminGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestConfirmAdmin'; };
            ActivityRequestConfirmAdminGrid.prototype.getDialogType = function () { return Case.ActivityRequestConfirmAdminDialog; };
            ActivityRequestConfirmAdminGrid.prototype.getIdProperty = function () { return Case.ActivityRequestConfirmAdminRow.idProperty; };
            ActivityRequestConfirmAdminGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestConfirmAdminRow.localTextPrefix; };
            ActivityRequestConfirmAdminGrid.prototype.getService = function () { return Case.ActivityRequestConfirmAdminService.baseUrl; };
            ActivityRequestConfirmAdminGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('CycleCost'),
                        new Slick.Aggregators.Sum('DelayedCost'),
                        new Slick.Aggregators.Sum('TotalLeakage'),
                        new Slick.Aggregators.Sum('RecoverableLeakage'),
                        new Slick.Aggregators.Sum('Recovered')
                    ]
                });
                return grid;
            };
            ActivityRequestConfirmAdminGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestConfirmAdminGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestConfirmService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var CreateTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(CreateTime_Start);
                        var CreateTime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(CreateTime_End);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        //var EndTime_Start = AllFilters[5].getElementsByTagName('input')[0].value; //console.log(EndTime_Start);
                        //var EndTime_End = AllFilters[5].getElementsByTagName('input')[1].value; //console.log(AllFilters[5].getElementsByTagName('input')[1].innerHTML);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        }
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        }
                        // window.location.href = "../Common/ActivityRequestConfirmPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                        //  + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                        window.location.href = "../Common/ActivityRequestConfirmPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&CreateTime_Start=" + CreateTime_Start + "&CreateTime_End=" + CreateTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestConfirmAdminGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestConfirmAdminGrid);
            return ActivityRequestConfirmAdminGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestConfirmAdminGrid = ActivityRequestConfirmAdminGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDeleteDialog = (function (_super) {
            __extends(ActivityRequestDeleteDialog, _super);
            function ActivityRequestDeleteDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityRequestDeleteForm(this.idPrefix);
            }
            ActivityRequestDeleteDialog.prototype.getFormKey = function () { return Case.ActivityRequestDeleteForm.formKey; };
            ActivityRequestDeleteDialog.prototype.getIdProperty = function () { return Case.ActivityRequestDeleteRow.idProperty; };
            ActivityRequestDeleteDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDeleteRow.localTextPrefix; };
            ActivityRequestDeleteDialog.prototype.getNameProperty = function () { return Case.ActivityRequestDeleteRow.nameProperty; };
            ActivityRequestDeleteDialog.prototype.getService = function () { return Case.ActivityRequestDeleteService.baseUrl; };
            ActivityRequestDeleteDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                return buttons;
            };
            ActivityRequestDeleteDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
            };
            ActivityRequestDeleteDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestDeleteDialog);
            return ActivityRequestDeleteDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestDeleteDialog = ActivityRequestDeleteDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDeleteGrid = (function (_super) {
            __extends(ActivityRequestDeleteGrid, _super);
            function ActivityRequestDeleteGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestDeleteGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestDelete'; };
            ActivityRequestDeleteGrid.prototype.getDialogType = function () { return Case.ActivityRequestDeleteDialog; };
            ActivityRequestDeleteGrid.prototype.getIdProperty = function () { return Case.ActivityRequestDeleteRow.idProperty; };
            ActivityRequestDeleteGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDeleteRow.localTextPrefix; };
            ActivityRequestDeleteGrid.prototype.getService = function () { return Case.ActivityRequestDeleteService.baseUrl; };
            ActivityRequestDeleteGrid.prototype.getButtons = function () {
                var buttons = _super.prototype.getButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestDeleteGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestDeleteGrid);
            return ActivityRequestDeleteGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestDeleteGrid = ActivityRequestDeleteGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDenyDialog = (function (_super) {
            __extends(ActivityRequestDenyDialog, _super);
            function ActivityRequestDenyDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestDenyForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ActivityRequestDenyDialog.prototype.getFormKey = function () { return Case.ActivityRequestDenyForm.formKey; };
            ActivityRequestDenyDialog.prototype.getIdProperty = function () { return Case.ActivityRequestDenyRow.idProperty; };
            ActivityRequestDenyDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDenyRow.localTextPrefix; };
            ActivityRequestDenyDialog.prototype.getService = function () { return Case.ActivityRequestDenyService.baseUrl; };
            ActivityRequestDenyDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                // We could also remove delete button here, but for demonstration 
                // purposes we'll hide it in another method (updateInterface)
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            ActivityRequestDenyDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            ActivityRequestDenyDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestDenyDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestDenyDialog);
            return ActivityRequestDenyDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestDenyDialog = ActivityRequestDenyDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDenyGrid = (function (_super) {
            __extends(ActivityRequestDenyGrid, _super);
            function ActivityRequestDenyGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestDenyGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestDeny'; };
            ActivityRequestDenyGrid.prototype.getDialogType = function () { return Case.ActivityRequestDenyDialog; };
            ActivityRequestDenyGrid.prototype.getIdProperty = function () { return Case.ActivityRequestDenyRow.idProperty; };
            ActivityRequestDenyGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDenyRow.localTextPrefix; };
            ActivityRequestDenyGrid.prototype.getService = function () { return Case.ActivityRequestDenyService.baseUrl; };
            ActivityRequestDenyGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage')
                    ]
                });
                return grid;
            };
            ActivityRequestDenyGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestDenyGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestDenyService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } //console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestDenyPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestDenyGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestDenyGrid);
            return ActivityRequestDenyGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestDenyGrid = ActivityRequestDenyGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDetailsInfoDialog = (function (_super) {
            __extends(ActivityRequestDetailsInfoDialog, _super);
            function ActivityRequestDetailsInfoDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityRequestDetailsInfoForm(this.idPrefix);
            }
            ActivityRequestDetailsInfoDialog.prototype.getFormKey = function () { return Case.ActivityRequestDetailsInfoForm.formKey; };
            ActivityRequestDetailsInfoDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDetailsInfoRow.localTextPrefix; };
            ActivityRequestDetailsInfoDialog.prototype.getNameProperty = function () { return Case.ActivityRequestDetailsInfoRow.nameProperty; };
            ActivityRequestDetailsInfoDialog.prototype.getService = function () { return Case.ActivityRequestDetailsInfoService.baseUrl; };
            ActivityRequestDetailsInfoDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestDetailsInfoDialog);
            return ActivityRequestDetailsInfoDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestDetailsInfoDialog = ActivityRequestDetailsInfoDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDetailsInfoGrid = (function (_super) {
            __extends(ActivityRequestDetailsInfoGrid, _super);
            function ActivityRequestDetailsInfoGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestDetailsInfoGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestDetailsInfo'; };
            ActivityRequestDetailsInfoGrid.prototype.getDialogType = function () { return Case.ActivityRequestDetailsInfoDialog; };
            ActivityRequestDetailsInfoGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestDetailsInfoRow.localTextPrefix; };
            ActivityRequestDetailsInfoGrid.prototype.getService = function () { return Case.ActivityRequestDetailsInfoService.baseUrl; };
            ActivityRequestDetailsInfoGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestDetailsInfoGrid);
            return ActivityRequestDetailsInfoGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestDetailsInfoGrid = ActivityRequestDetailsInfoGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestFinancialDialog = (function (_super) {
            __extends(ActivityRequestFinancialDialog, _super);
            function ActivityRequestFinancialDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestFinancialForm(this.idPrefix);
                //   this.activityRequestHistoryPropertyGrid = new Serenity.PropertyGrid(this.byId("ActivityRequestHistoryPropertyGrid"), {
                //       items: Q.getForm(ActivityRequestHistoryForm.formKey).filter(x => x.Id != 'Id'),
                //       useCategories: true
                //   });
                //
                //   // this is just a helper to access editors if needed
                //   this.activityRequestHistoryForm = new ActivityRequestHistoryForm((this.activityRequestHistoryPropertyGrid as any).idPrefix);
                //   this.historyValidator = this.byId("ActivityRequestHistoryForm").validate(Q.validateOptions({}));
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.form = new Case.ActivityRequestFinancialForm(this.idPrefix);
                this.form.ActivityId.changeSelect2(function (e) {
                    var ActivityId = Q.toId(_this.form.ActivityId.value);
                    if (ActivityId != null) {
                        var RequiredYearRepeatCOUNT = Case.ActivityRow.getLookup().itemById[ActivityId].RequiredYearRepeatCount;
                        _this.form.Factor.value = RequiredYearRepeatCOUNT.toString();
                    }
                });
                this.form.CycleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var factor = Q.toId(_this.form.Factor.value);
                    if (cycle != null && factor != null) {
                        var year = cycle * factor;
                        _this.form.YearCost.value = year.toString();
                    }
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                    var accessible = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessible != null) {
                        var recoverableLeakage = cycle + accessible;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.DelayedCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                });
                this.form.AccessibleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var accessibleCost = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessibleCost != null) {
                        var recoverableLeakage = cycle + accessibleCost;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.ActivityId.changeSelect2(function (e) {
                    var activityID = Q.toId(_this.form.ActivityId.value);
                    if (activityID != null) {
                        _this.form.EventDescription.value = Case.ActivityRow.getLookup().itemById[activityID].EventDescription;
                    }
                });
            }
            ActivityRequestFinancialDialog.prototype.getFormKey = function () { return Case.ActivityRequestFinancialForm.formKey; };
            ActivityRequestFinancialDialog.prototype.getIdProperty = function () { return Case.ActivityRequestFinancialRow.idProperty; };
            ActivityRequestFinancialDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestFinancialRow.localTextPrefix; };
            ActivityRequestFinancialDialog.prototype.getService = function () { return Case.ActivityRequestFinancialService.baseUrl; };
            // loadEntity(entity: ActivityRequestFinancialRow) {
            //     super.loadEntity(entity);
            //
            //     Serenity.TabsExtensions.setDisabled(this.tabs, 'ActivityRequestHistory',
            //         !this.form.Id.value);
            //
            //     this.activityRequestHistoryPropertyGrid.load({});
            // }
            ActivityRequestFinancialDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                this.deleteButton.hide();
            };
            ActivityRequestFinancialDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestFinancialDialog = __decorate([
                Serenity.Decorators.maximizable(),
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestFinancialDialog);
            return ActivityRequestFinancialDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestFinancialDialog = ActivityRequestFinancialDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestFinancialGrid = (function (_super) {
            __extends(ActivityRequestFinancialGrid, _super);
            function ActivityRequestFinancialGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestFinancialGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestFinancial'; };
            ActivityRequestFinancialGrid.prototype.getDialogType = function () { return Case.ActivityRequestFinancialDialog; };
            ActivityRequestFinancialGrid.prototype.getIdProperty = function () { return Case.ActivityRequestFinancialRow.idProperty; };
            ActivityRequestFinancialGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestFinancialRow.localTextPrefix; };
            ActivityRequestFinancialGrid.prototype.getService = function () { return Case.ActivityRequestFinancialService.baseUrl; };
            ActivityRequestFinancialGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage')
                    ]
                });
                return grid;
            };
            ActivityRequestFinancialGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestFinancialGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestFinancialService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } // console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestFinancialPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestFinancialGrid.prototype.getItemCssClass = function (item, index) {
                var klass = "";
                if (item.IsRejected == true)
                    klass += "actionReject";
                return Q.trimToNull(klass);
            };
            ActivityRequestFinancialGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestFinancialGrid);
            return ActivityRequestFinancialGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestFinancialGrid = ActivityRequestFinancialGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestHistoryDialog = (function (_super) {
            __extends(ActivityRequestHistoryDialog, _super);
            function ActivityRequestHistoryDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityRequestHistoryForm(this.idPrefix);
            }
            ActivityRequestHistoryDialog.prototype.getFormKey = function () { return Case.ActivityRequestHistoryForm.formKey; };
            ActivityRequestHistoryDialog.prototype.getIdProperty = function () { return Case.ActivityRequestHistoryRow.idProperty; };
            ActivityRequestHistoryDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestHistoryRow.localTextPrefix; };
            ActivityRequestHistoryDialog.prototype.getNameProperty = function () { return Case.ActivityRequestHistoryRow.nameProperty; };
            ActivityRequestHistoryDialog.prototype.getService = function () { return Case.ActivityRequestHistoryService.baseUrl; };
            ActivityRequestHistoryDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestHistoryDialog);
            return ActivityRequestHistoryDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestHistoryDialog = ActivityRequestHistoryDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestHistoryGrid = (function (_super) {
            __extends(ActivityRequestHistoryGrid, _super);
            function ActivityRequestHistoryGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestHistoryGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestHistory'; };
            ActivityRequestHistoryGrid.prototype.getDialogType = function () { return Case.ActivityRequestHistoryDialog; };
            ActivityRequestHistoryGrid.prototype.getIdProperty = function () { return Case.ActivityRequestHistoryRow.idProperty; };
            ActivityRequestHistoryGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestHistoryRow.localTextPrefix; };
            ActivityRequestHistoryGrid.prototype.getService = function () { return Case.ActivityRequestHistoryService.baseUrl; };
            ActivityRequestHistoryGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestHistoryGrid);
            return ActivityRequestHistoryGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestHistoryGrid = ActivityRequestHistoryGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLeaderDialog = (function (_super) {
            __extends(ActivityRequestLeaderDialog, _super);
            function ActivityRequestLeaderDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestLeaderForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ActivityRequestLeaderDialog.prototype.getFormKey = function () { return Case.ActivityRequestLeaderForm.formKey; };
            ActivityRequestLeaderDialog.prototype.getIdProperty = function () { return Case.ActivityRequestLeaderRow.idProperty; };
            ActivityRequestLeaderDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestLeaderRow.localTextPrefix; };
            ActivityRequestLeaderDialog.prototype.getService = function () { return Case.ActivityRequestLeaderService.baseUrl; };
            ActivityRequestLeaderDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                // We could also remove delete button here, but for demonstration 
                // purposes we'll hide it in another method (updateInterface)
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            ActivityRequestLeaderDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            ActivityRequestLeaderDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestLeaderDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestLeaderDialog);
            return ActivityRequestLeaderDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestLeaderDialog = ActivityRequestLeaderDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLeaderGrid = (function (_super) {
            __extends(ActivityRequestLeaderGrid, _super);
            function ActivityRequestLeaderGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestLeaderGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestLeader'; };
            ActivityRequestLeaderGrid.prototype.getDialogType = function () { return Case.ActivityRequestLeaderDialog; };
            ActivityRequestLeaderGrid.prototype.getIdProperty = function () { return Case.ActivityRequestLeaderRow.idProperty; };
            ActivityRequestLeaderGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestLeaderRow.localTextPrefix; };
            ActivityRequestLeaderGrid.prototype.getService = function () { return Case.ActivityRequestLeaderService.baseUrl; };
            ActivityRequestLeaderGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage')
                    ]
                });
                return grid;
            };
            ActivityRequestLeaderGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestLeaderGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestLeaderService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } //console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestLeaderPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestLeaderGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestLeaderGrid);
            return ActivityRequestLeaderGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestLeaderGrid = ActivityRequestLeaderGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLogDialog = (function (_super) {
            __extends(ActivityRequestLogDialog, _super);
            function ActivityRequestLogDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ActivityRequestLogForm(this.idPrefix);
            }
            ActivityRequestLogDialog.prototype.getFormKey = function () { return Case.ActivityRequestLogForm.formKey; };
            ActivityRequestLogDialog.prototype.getIdProperty = function () { return Case.ActivityRequestLogRow.idProperty; };
            ActivityRequestLogDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestLogRow.localTextPrefix; };
            ActivityRequestLogDialog.prototype.getService = function () { return Case.ActivityRequestLogService.baseUrl; };
            ActivityRequestLogDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestLogDialog);
            return ActivityRequestLogDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestLogDialog = ActivityRequestLogDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestPenddingDialog = (function (_super) {
            __extends(ActivityRequestPenddingDialog, _super);
            function ActivityRequestPenddingDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestPenddingForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ActivityRequestPenddingDialog.prototype.getFormKey = function () { return Case.ActivityRequestPenddingForm.formKey; };
            ActivityRequestPenddingDialog.prototype.getIdProperty = function () { return Case.ActivityRequestPenddingRow.idProperty; };
            ActivityRequestPenddingDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestPenddingRow.localTextPrefix; };
            ActivityRequestPenddingDialog.prototype.getService = function () { return Case.ActivityRequestPenddingService.baseUrl; };
            ActivityRequestPenddingDialog.prototype.getToolbarButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.push({
                    title: Q.text('چاپ فعالیت'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var activityID = _this.form.Id.value;
                        window.location.href = "../Common/ActivityRequestPendingInfoTOPrint?ActivityId=" + activityID;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                // We could also remove delete button here, but for demonstration 
                // purposes we'll hide it in another method (updateInterface)
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            ActivityRequestPenddingDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            ActivityRequestPenddingDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestPenddingDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestPenddingDialog);
            return ActivityRequestPenddingDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestPenddingDialog = ActivityRequestPenddingDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestPenddingGrid = (function (_super) {
            __extends(ActivityRequestPenddingGrid, _super);
            function ActivityRequestPenddingGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestPenddingGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestPendding'; };
            ActivityRequestPenddingGrid.prototype.getDialogType = function () { return Case.ActivityRequestPenddingDialog; };
            ActivityRequestPenddingGrid.prototype.getIdProperty = function () { return Case.ActivityRequestPenddingRow.idProperty; };
            ActivityRequestPenddingGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestPenddingRow.localTextPrefix; };
            ActivityRequestPenddingGrid.prototype.getService = function () { return Case.ActivityRequestPenddingService.baseUrl; };
            ActivityRequestPenddingGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage')
                    ]
                });
                return grid;
            };
            ActivityRequestPenddingGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestPenddingGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestPenddingService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } //console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestPendingPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestPenddingGrid.prototype.getItemCssClass = function (item, index) {
                var klass = "";
                // alert(item.Id)
                //  if (item.IsRejected == true)
                //      klass += " actionReject";
                return Q.trimToNull(klass);
            };
            ActivityRequestPenddingGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestPenddingGrid);
            return ActivityRequestPenddingGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestPenddingGrid = ActivityRequestPenddingGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestTechnicalDialog = (function (_super) {
            __extends(ActivityRequestTechnicalDialog, _super);
            function ActivityRequestTechnicalDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ActivityRequestTechnicalForm(this.idPrefix);
                this.logsGrid = new Case.ActivityRequestLogGrid(this.byId("LogsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.form = new Case.ActivityRequestTechnicalForm(this.idPrefix);
                this.form.ActivityId.changeSelect2(function (e) {
                    var ActivityId = Q.toId(_this.form.ActivityId.value);
                    if (ActivityId != null) {
                        var RequiredYearRepeatCOUNT = Case.ActivityRow.getLookup().itemById[ActivityId].RequiredYearRepeatCount;
                        _this.form.Factor.value = RequiredYearRepeatCOUNT.toString();
                    }
                });
                this.form.CycleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var factor = Q.toId(_this.form.Factor.value);
                    if (cycle != null && factor != null) {
                        var year = cycle * factor;
                        _this.form.YearCost.value = year.toString();
                    }
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                    var accessible = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessible != null) {
                        var recoverableLeakage = cycle + accessible;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.DelayedCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var delay = Q.toId(_this.form.DelayedCost.value);
                    if (cycle != null && delay != null) {
                        var total = cycle + delay;
                        _this.form.TotalLeakage.value = total.toString();
                    }
                });
                this.form.AccessibleCost.change(function (e) {
                    var cycle = Q.toId(_this.form.CycleCost.value);
                    var accessibleCost = Q.toId(_this.form.AccessibleCost.value);
                    if (cycle != null && accessibleCost != null) {
                        var recoverableLeakage = cycle + accessibleCost;
                        _this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                        _this.form.Recovered.value = recoverableLeakage.toString();
                    }
                });
                this.form.ActivityId.changeSelect2(function (e) {
                    var activityID = Q.toId(_this.form.ActivityId.value);
                    if (activityID != null) {
                        _this.form.EventDescription.value = Case.ActivityRow.getLookup().itemById[activityID].EventDescription;
                    }
                });
            }
            ActivityRequestTechnicalDialog.prototype.getFormKey = function () { return Case.ActivityRequestTechnicalForm.formKey; };
            ActivityRequestTechnicalDialog.prototype.getIdProperty = function () { return Case.ActivityRequestTechnicalRow.idProperty; };
            ActivityRequestTechnicalDialog.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestTechnicalRow.localTextPrefix; };
            ActivityRequestTechnicalDialog.prototype.getService = function () { return Case.ActivityRequestTechnicalService.baseUrl; };
            /*  protected getToolbarButtons(): Serenity.ToolButton[] {
                  let buttons = super.getToolbarButtons();
                  buttons.push({
                      title: Q.text('چاپ'),
                      cssClass: 'print-preview-button',
                      onClick: () => {
                          var activityID = this.form.Id.value;
                          window.location.href = "../Common/ActivityRequestTechnicalInfoTOPrint?ActivityId=" + activityID ;
                      }
                  });
      
                  return buttons;
              }*/
            ActivityRequestTechnicalDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                this.deleteButton.hide();
            };
            ActivityRequestTechnicalDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                this.logsGrid.ActivityRequestID = this.entityId;
            };
            ActivityRequestTechnicalDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestTechnicalDialog);
            return ActivityRequestTechnicalDialog;
        }(Serenity.EntityDialog));
        Case.ActivityRequestTechnicalDialog = ActivityRequestTechnicalDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestTechnicalGrid = (function (_super) {
            __extends(ActivityRequestTechnicalGrid, _super);
            function ActivityRequestTechnicalGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestTechnicalGrid.prototype.getColumnsKey = function () { return 'Case.ActivityRequestTechnical'; };
            ActivityRequestTechnicalGrid.prototype.getDialogType = function () { return Case.ActivityRequestTechnicalDialog; };
            ActivityRequestTechnicalGrid.prototype.getIdProperty = function () { return Case.ActivityRequestTechnicalRow.idProperty; };
            ActivityRequestTechnicalGrid.prototype.getLocalTextPrefix = function () { return Case.ActivityRequestTechnicalRow.localTextPrefix; };
            ActivityRequestTechnicalGrid.prototype.getService = function () { return Case.ActivityRequestTechnicalService.baseUrl; };
            ActivityRequestTechnicalGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage')
                    ]
                });
                return grid;
            };
            ActivityRequestTechnicalGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestTechnicalGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ActivityRequestTechnicalService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        var IncomeFlow = document.getElementById("select2-chosen-3").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } //console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestTechnicalPrint?ActivityCode=" + ActivityCode + "&DiscoverTime_Start=" + DiscoverTime_Start
                            + "&DiscoverTime_End=" + DiscoverTime_End + "&Province=" + Province + "&Cycle=" + Cycle + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                return buttons;
            };
            ActivityRequestTechnicalGrid.prototype.getItemCssClass = function (item, index) {
                var klass = "";
                if (item.IsRejected == true)
                    klass += "actionReject";
                return Q.trimToNull(klass);
            };
            ActivityRequestTechnicalGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestTechnicalGrid);
            return ActivityRequestTechnicalGrid;
        }(Serenity.EntityGrid));
        Case.ActivityRequestTechnicalGrid = ActivityRequestTechnicalGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CommentReasonDialog = (function (_super) {
            __extends(CommentReasonDialog, _super);
            function CommentReasonDialog() {
                _super.apply(this, arguments);
                this.form = new Case.CommentReasonForm(this.idPrefix);
            }
            CommentReasonDialog.prototype.getFormKey = function () { return Case.CommentReasonForm.formKey; };
            CommentReasonDialog.prototype.getIdProperty = function () { return Case.CommentReasonRow.idProperty; };
            CommentReasonDialog.prototype.getLocalTextPrefix = function () { return Case.CommentReasonRow.localTextPrefix; };
            CommentReasonDialog.prototype.getNameProperty = function () { return Case.CommentReasonRow.nameProperty; };
            CommentReasonDialog.prototype.getService = function () { return Case.CommentReasonService.baseUrl; };
            CommentReasonDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], CommentReasonDialog);
            return CommentReasonDialog;
        }(Serenity.EntityDialog));
        Case.CommentReasonDialog = CommentReasonDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CommentReasonGrid = (function (_super) {
            __extends(CommentReasonGrid, _super);
            function CommentReasonGrid(container) {
                _super.call(this, container);
            }
            CommentReasonGrid.prototype.getColumnsKey = function () { return 'Case.CommentReason'; };
            CommentReasonGrid.prototype.getDialogType = function () { return Case.CommentReasonDialog; };
            CommentReasonGrid.prototype.getIdProperty = function () { return Case.CommentReasonRow.idProperty; };
            CommentReasonGrid.prototype.getLocalTextPrefix = function () { return Case.CommentReasonRow.localTextPrefix; };
            CommentReasonGrid.prototype.getService = function () { return Case.CommentReasonService.baseUrl; };
            CommentReasonGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], CommentReasonGrid);
            return CommentReasonGrid;
        }(Serenity.EntityGrid));
        Case.CommentReasonGrid = CommentReasonGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CompanyDialog = (function (_super) {
            __extends(CompanyDialog, _super);
            function CompanyDialog() {
                _super.apply(this, arguments);
                this.form = new Case.CompanyForm(this.idPrefix);
            }
            CompanyDialog.prototype.getFormKey = function () { return Case.CompanyForm.formKey; };
            CompanyDialog.prototype.getIdProperty = function () { return Case.CompanyRow.idProperty; };
            CompanyDialog.prototype.getLocalTextPrefix = function () { return Case.CompanyRow.localTextPrefix; };
            CompanyDialog.prototype.getNameProperty = function () { return Case.CompanyRow.nameProperty; };
            CompanyDialog.prototype.getService = function () { return Case.CompanyService.baseUrl; };
            CompanyDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], CompanyDialog);
            return CompanyDialog;
        }(Serenity.EntityDialog));
        Case.CompanyDialog = CompanyDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CompanyGrid = (function (_super) {
            __extends(CompanyGrid, _super);
            function CompanyGrid(container) {
                _super.call(this, container);
            }
            CompanyGrid.prototype.getColumnsKey = function () { return 'Case.Company'; };
            CompanyGrid.prototype.getDialogType = function () { return Case.CompanyDialog; };
            CompanyGrid.prototype.getIdProperty = function () { return Case.CompanyRow.idProperty; };
            CompanyGrid.prototype.getLocalTextPrefix = function () { return Case.CompanyRow.localTextPrefix; };
            CompanyGrid.prototype.getService = function () { return Case.CompanyService.baseUrl; };
            CompanyGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], CompanyGrid);
            return CompanyGrid;
        }(Serenity.EntityGrid));
        Case.CompanyGrid = CompanyGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CustomerEffectDialog = (function (_super) {
            __extends(CustomerEffectDialog, _super);
            function CustomerEffectDialog() {
                _super.apply(this, arguments);
                this.form = new Case.CustomerEffectForm(this.idPrefix);
            }
            CustomerEffectDialog.prototype.getFormKey = function () { return Case.CustomerEffectForm.formKey; };
            CustomerEffectDialog.prototype.getIdProperty = function () { return Case.CustomerEffectRow.idProperty; };
            CustomerEffectDialog.prototype.getLocalTextPrefix = function () { return Case.CustomerEffectRow.localTextPrefix; };
            CustomerEffectDialog.prototype.getNameProperty = function () { return Case.CustomerEffectRow.nameProperty; };
            CustomerEffectDialog.prototype.getService = function () { return Case.CustomerEffectService.baseUrl; };
            CustomerEffectDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], CustomerEffectDialog);
            return CustomerEffectDialog;
        }(Serenity.EntityDialog));
        Case.CustomerEffectDialog = CustomerEffectDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CustomerEffectGrid = (function (_super) {
            __extends(CustomerEffectGrid, _super);
            function CustomerEffectGrid(container) {
                _super.call(this, container);
            }
            CustomerEffectGrid.prototype.getColumnsKey = function () { return 'Case.CustomerEffect'; };
            CustomerEffectGrid.prototype.getDialogType = function () { return Case.CustomerEffectDialog; };
            CustomerEffectGrid.prototype.getIdProperty = function () { return Case.CustomerEffectRow.idProperty; };
            CustomerEffectGrid.prototype.getLocalTextPrefix = function () { return Case.CustomerEffectRow.localTextPrefix; };
            CustomerEffectGrid.prototype.getService = function () { return Case.CustomerEffectService.baseUrl; };
            CustomerEffectGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], CustomerEffectGrid);
            return CustomerEffectGrid;
        }(Serenity.EntityGrid));
        Case.CustomerEffectGrid = CustomerEffectGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CycleDialog = (function (_super) {
            __extends(CycleDialog, _super);
            function CycleDialog() {
                _super.apply(this, arguments);
                this.form = new Case.CycleForm(this.idPrefix);
            }
            CycleDialog.prototype.getFormKey = function () { return Case.CycleForm.formKey; };
            CycleDialog.prototype.getIdProperty = function () { return Case.CycleRow.idProperty; };
            CycleDialog.prototype.getLocalTextPrefix = function () { return Case.CycleRow.localTextPrefix; };
            CycleDialog.prototype.getNameProperty = function () { return Case.CycleRow.nameProperty; };
            CycleDialog.prototype.getService = function () { return Case.CycleService.baseUrl; };
            CycleDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], CycleDialog);
            return CycleDialog;
        }(Serenity.EntityDialog));
        Case.CycleDialog = CycleDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CycleGrid = (function (_super) {
            __extends(CycleGrid, _super);
            function CycleGrid(container) {
                _super.call(this, container);
            }
            CycleGrid.prototype.getColumnsKey = function () { return 'Case.Cycle'; };
            CycleGrid.prototype.getDialogType = function () { return Case.CycleDialog; };
            CycleGrid.prototype.getIdProperty = function () { return Case.CycleRow.idProperty; };
            CycleGrid.prototype.getLocalTextPrefix = function () { return Case.CycleRow.localTextPrefix; };
            CycleGrid.prototype.getService = function () { return Case.CycleService.baseUrl; };
            CycleGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], CycleGrid);
            return CycleGrid;
        }(Serenity.EntityGrid));
        Case.CycleGrid = CycleGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var IncomeFlowDialog = (function (_super) {
            __extends(IncomeFlowDialog, _super);
            function IncomeFlowDialog() {
                _super.apply(this, arguments);
                this.form = new Case.IncomeFlowForm(this.idPrefix);
            }
            IncomeFlowDialog.prototype.getFormKey = function () { return Case.IncomeFlowForm.formKey; };
            IncomeFlowDialog.prototype.getIdProperty = function () { return Case.IncomeFlowRow.idProperty; };
            IncomeFlowDialog.prototype.getLocalTextPrefix = function () { return Case.IncomeFlowRow.localTextPrefix; };
            IncomeFlowDialog.prototype.getNameProperty = function () { return Case.IncomeFlowRow.nameProperty; };
            IncomeFlowDialog.prototype.getService = function () { return Case.IncomeFlowService.baseUrl; };
            IncomeFlowDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], IncomeFlowDialog);
            return IncomeFlowDialog;
        }(Serenity.EntityDialog));
        Case.IncomeFlowDialog = IncomeFlowDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var IncomeFlowGrid = (function (_super) {
            __extends(IncomeFlowGrid, _super);
            function IncomeFlowGrid(container) {
                _super.call(this, container);
            }
            IncomeFlowGrid.prototype.getColumnsKey = function () { return 'Case.IncomeFlow'; };
            IncomeFlowGrid.prototype.getDialogType = function () { return Case.IncomeFlowDialog; };
            IncomeFlowGrid.prototype.getIdProperty = function () { return Case.IncomeFlowRow.idProperty; };
            IncomeFlowGrid.prototype.getLocalTextPrefix = function () { return Case.IncomeFlowRow.localTextPrefix; };
            IncomeFlowGrid.prototype.getService = function () { return Case.IncomeFlowService.baseUrl; };
            IncomeFlowGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], IncomeFlowGrid);
            return IncomeFlowGrid;
        }(Serenity.EntityGrid));
        Case.IncomeFlowGrid = IncomeFlowGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var PmoLevelDialog = (function (_super) {
            __extends(PmoLevelDialog, _super);
            function PmoLevelDialog() {
                _super.apply(this, arguments);
                this.form = new Case.PmoLevelForm(this.idPrefix);
            }
            PmoLevelDialog.prototype.getFormKey = function () { return Case.PmoLevelForm.formKey; };
            PmoLevelDialog.prototype.getIdProperty = function () { return Case.PmoLevelRow.idProperty; };
            PmoLevelDialog.prototype.getLocalTextPrefix = function () { return Case.PmoLevelRow.localTextPrefix; };
            PmoLevelDialog.prototype.getNameProperty = function () { return Case.PmoLevelRow.nameProperty; };
            PmoLevelDialog.prototype.getService = function () { return Case.PmoLevelService.baseUrl; };
            PmoLevelDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], PmoLevelDialog);
            return PmoLevelDialog;
        }(Serenity.EntityDialog));
        Case.PmoLevelDialog = PmoLevelDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var PmoLevelGrid = (function (_super) {
            __extends(PmoLevelGrid, _super);
            function PmoLevelGrid(container) {
                _super.call(this, container);
            }
            PmoLevelGrid.prototype.getColumnsKey = function () { return 'Case.PmoLevel'; };
            PmoLevelGrid.prototype.getDialogType = function () { return Case.PmoLevelDialog; };
            PmoLevelGrid.prototype.getIdProperty = function () { return Case.PmoLevelRow.idProperty; };
            PmoLevelGrid.prototype.getLocalTextPrefix = function () { return Case.PmoLevelRow.localTextPrefix; };
            PmoLevelGrid.prototype.getService = function () { return Case.PmoLevelService.baseUrl; };
            PmoLevelGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], PmoLevelGrid);
            return PmoLevelGrid;
        }(Serenity.EntityGrid));
        Case.PmoLevelGrid = PmoLevelGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceCompanySoftwareGrid = (function (_super) {
            __extends(ProvinceCompanySoftwareGrid, _super);
            function ProvinceCompanySoftwareGrid(container) {
                _super.call(this, container);
            }
            ProvinceCompanySoftwareGrid.prototype.getColumnsKey = function () { return 'Case.ProvinceCompanySoftware'; };
            //protected getDialogType() { return ProvinceCompanySoftwareDialog; }
            ProvinceCompanySoftwareGrid.prototype.getIdProperty = function () { return Case.ProvinceCompanySoftwareRow.idProperty; };
            ProvinceCompanySoftwareGrid.prototype.getLocalTextPrefix = function () { return Case.ProvinceCompanySoftwareRow.localTextPrefix; };
            ProvinceCompanySoftwareGrid.prototype.getService = function () { return Case.ProvinceCompanySoftwareService.baseUrl; };
            ProvinceCompanySoftwareGrid.prototype.getButtons = function () {
                return null;
            };
            ProvinceCompanySoftwareGrid.prototype.getInitialTitle = function () {
                return null;
            };
            ProvinceCompanySoftwareGrid.prototype.usePager = function () {
                return false;
            };
            ProvinceCompanySoftwareGrid.prototype.getGridCanLoad = function () {
                return this.ProvinceID != null;
            };
            Object.defineProperty(ProvinceCompanySoftwareGrid.prototype, "ProvinceID", {
                get: function () {
                    return this._ProvinceID;
                },
                set: function (value) {
                    if (this._ProvinceID != value) {
                        this._ProvinceID = value;
                        this.setEquality(Case.ProvinceCompanySoftwareRow.Fields.ProvinveId, value);
                        this.refresh();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ProvinceCompanySoftwareGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceCompanySoftwareGrid);
            return ProvinceCompanySoftwareGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceCompanySoftwareGrid = ProvinceCompanySoftwareGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceDialog = (function (_super) {
            __extends(ProvinceDialog, _super);
            function ProvinceDialog() {
                var _this = this;
                _super.call(this);
                this.form = new Case.ProvinceForm(this.idPrefix);
                this.switchsGrid = new Case.ProvinceSwitchGrid(this.byId("SwitchsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.switchDSLAMsGrid = new Case.ProvinceSwitchDSLAMGrid(this.byId("SwitchDSLAMsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.switchTransitsGrid = new Case.ProvinceSwitchTransitGrid(this.byId("SwitchTransitsGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
                this.softwaresGrid = new Case.ProvinceCompanySoftwareGrid(this.byId("SoftwaresGrid"));
                this.tabs.on('tabsactivate', function (e, i) {
                    _this.arrange();
                });
            }
            ProvinceDialog.prototype.getFormKey = function () { return Case.ProvinceForm.formKey; };
            ProvinceDialog.prototype.getIdProperty = function () { return Case.ProvinceRow.idProperty; };
            ProvinceDialog.prototype.getLocalTextPrefix = function () { return Case.ProvinceRow.localTextPrefix; };
            ProvinceDialog.prototype.getNameProperty = function () { return Case.ProvinceRow.nameProperty; };
            ProvinceDialog.prototype.getService = function () { return Case.ProvinceService.baseUrl; };
            ProvinceDialog.prototype.afterLoadEntity = function () {
                _super.prototype.afterLoadEntity.call(this);
                Serenity.TabsExtensions.setDisabled(this.tabs, 'Orders', this.isNewOrDeleted());
                this.switchsGrid.ProvinceID = this.entityId;
                this.switchDSLAMsGrid.ProvinceID = this.entityId;
                this.switchTransitsGrid.ProvinceID = this.entityId;
                this.softwaresGrid.ProvinceID = this.entityId;
            };
            ProvinceDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ProvinceDialog);
            return ProvinceDialog;
        }(Serenity.EntityDialog));
        Case.ProvinceDialog = ProvinceDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceGrid = (function (_super) {
            __extends(ProvinceGrid, _super);
            function ProvinceGrid(container) {
                _super.call(this, container);
            }
            ProvinceGrid.prototype.getColumnsKey = function () { return 'Case.Province'; };
            ProvinceGrid.prototype.getDialogType = function () { return Case.ProvinceDialog; };
            ProvinceGrid.prototype.getIdProperty = function () { return Case.ProvinceRow.idProperty; };
            ProvinceGrid.prototype.getLocalTextPrefix = function () { return Case.ProvinceRow.localTextPrefix; };
            ProvinceGrid.prototype.getService = function () { return Case.ProvinceService.baseUrl; };
            ProvinceGrid.prototype.getButtons = function () {
                //var buttons = super.getButtons();
                var _this = this;
                //buttons.push(Common.ExcelExportHelper.createToolButton({
                //    grid: this,
                //    service: ProvinceService.baseUrl + '/ListExcel',
                //    onViewSubmit: () => this.onViewSubmit(),
                //    separator: true
                //}));
                //return buttons;
                return [{
                        title: 'دسته بندی سرگروه',
                        cssClass: 'expand-all-button',
                        onClick: function () { return _this.view.setGrouping([{
                                getter: 'LeaderName'
                            }]); }
                    },
                    {
                        title: 'بدون گروه بندی',
                        cssClass: 'collapse-all-button',
                        onClick: function () { return _this.view.setGrouping([]); }
                    }];
            };
            ProvinceGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceGrid);
            return ProvinceGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceGrid = ProvinceGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceSwitchDSLAMGrid = (function (_super) {
            __extends(ProvinceSwitchDSLAMGrid, _super);
            function ProvinceSwitchDSLAMGrid(container) {
                _super.call(this, container);
            }
            ProvinceSwitchDSLAMGrid.prototype.getColumnsKey = function () { return 'Case.SwitchDslamProvince'; };
            //protected getDialogType() { return SwitchProvinceDialog; }
            ProvinceSwitchDSLAMGrid.prototype.getIdProperty = function () { return Case.SwitchDslamProvinceRow.idProperty; };
            ProvinceSwitchDSLAMGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchDslamProvinceRow.localTextPrefix; };
            ProvinceSwitchDSLAMGrid.prototype.getService = function () { return Case.SwitchDslamProvinceService.baseUrl; };
            ProvinceSwitchDSLAMGrid.prototype.getButtons = function () {
                return null;
            };
            ProvinceSwitchDSLAMGrid.prototype.getInitialTitle = function () {
                return null;
            };
            ProvinceSwitchDSLAMGrid.prototype.usePager = function () {
                return false;
            };
            ProvinceSwitchDSLAMGrid.prototype.getGridCanLoad = function () {
                return this.ProvinceID != null;
            };
            Object.defineProperty(ProvinceSwitchDSLAMGrid.prototype, "ProvinceID", {
                get: function () {
                    return this._ProvinceID;
                },
                set: function (value) {
                    if (this._ProvinceID != value) {
                        this._ProvinceID = value;
                        this.setEquality(Case.SwitchDslamProvinceRow.Fields.ProvinceId, value);
                        this.refresh();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ProvinceSwitchDSLAMGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceSwitchDSLAMGrid);
            return ProvinceSwitchDSLAMGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceSwitchDSLAMGrid = ProvinceSwitchDSLAMGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceSwitchGrid = (function (_super) {
            __extends(ProvinceSwitchGrid, _super);
            function ProvinceSwitchGrid(container) {
                _super.call(this, container);
            }
            ProvinceSwitchGrid.prototype.getColumnsKey = function () { return 'Case.SwitchProvince'; };
            //protected getDialogType() { return SwitchProvinceDialog; }
            ProvinceSwitchGrid.prototype.getIdProperty = function () { return Case.SwitchProvinceRow.idProperty; };
            ProvinceSwitchGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchProvinceRow.localTextPrefix; };
            ProvinceSwitchGrid.prototype.getService = function () { return Case.SwitchProvinceService.baseUrl; };
            ProvinceSwitchGrid.prototype.getButtons = function () {
                return null;
            };
            ProvinceSwitchGrid.prototype.getInitialTitle = function () {
                return null;
            };
            ProvinceSwitchGrid.prototype.usePager = function () {
                return false;
            };
            ProvinceSwitchGrid.prototype.getGridCanLoad = function () {
                return this.ProvinceID != null;
            };
            Object.defineProperty(ProvinceSwitchGrid.prototype, "ProvinceID", {
                get: function () {
                    return this._ProvinceID;
                },
                set: function (value) {
                    if (this._ProvinceID != value) {
                        this._ProvinceID = value;
                        this.setEquality(Case.SwitchProvinceRow.Fields.ProvinceId, value);
                        this.refresh();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ProvinceSwitchGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceSwitchGrid);
            return ProvinceSwitchGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceSwitchGrid = ProvinceSwitchGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceSwitchTransitGrid = (function (_super) {
            __extends(ProvinceSwitchTransitGrid, _super);
            function ProvinceSwitchTransitGrid(container) {
                _super.call(this, container);
            }
            ProvinceSwitchTransitGrid.prototype.getColumnsKey = function () { return 'Case.SwitchTransitProvince'; };
            ProvinceSwitchTransitGrid.prototype.getDialogType = function () { return Case.SwitchTransitProvinceDialog; };
            ProvinceSwitchTransitGrid.prototype.getIdProperty = function () { return Case.SwitchTransitProvinceRow.idProperty; };
            ProvinceSwitchTransitGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchTransitProvinceRow.localTextPrefix; };
            ProvinceSwitchTransitGrid.prototype.getService = function () { return Case.SwitchTransitProvinceService.baseUrl; };
            ProvinceSwitchTransitGrid.prototype.getColumns = function () {
                var fld = Case.SwitchTransitProvinceRow.Fields;
                return _super.prototype.getColumns.call(this).filter(function (x) { return x.field !== fld.ProvinceName; });
            };
            ProvinceSwitchTransitGrid.prototype.initEntityDialog = function (itemType, dialog) {
                _super.prototype.initEntityDialog.call(this, itemType, dialog);
                Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
            };
            ProvinceSwitchTransitGrid.prototype.addButtonClick = function () {
                this.editItem({ ProvinceID: this.ProvinceID });
            };
            ProvinceSwitchTransitGrid.prototype.getInitialTitle = function () {
                return null;
            };
            ProvinceSwitchTransitGrid.prototype.usePager = function () {
                return false;
            };
            ProvinceSwitchTransitGrid.prototype.getGridCanLoad = function () {
                return _super.prototype.getGridCanLoad.call(this) && !!this.ProvinceID;
            };
            Object.defineProperty(ProvinceSwitchTransitGrid.prototype, "ProvinceID", {
                get: function () {
                    return this._ProvinceID;
                },
                set: function (value) {
                    if (this._ProvinceID != value) {
                        this._ProvinceID = value;
                        //this.setEquality(SwitchTransitProvinceRow.Fields.ProvinceId, value);
                        this.setEquality('ProvinceID', value);
                        this.refresh();
                    }
                },
                enumerable: true,
                configurable: true
            });
            ProvinceSwitchTransitGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceSwitchTransitGrid);
            return ProvinceSwitchTransitGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceSwitchTransitGrid = ProvinceSwitchTransitGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitProvinceDialog = (function (_super) {
            __extends(SwitchTransitProvinceDialog, _super);
            function SwitchTransitProvinceDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SwitchTransitProvinceForm(this.idPrefix);
            }
            SwitchTransitProvinceDialog.prototype.getFormKey = function () { return Case.SwitchTransitProvinceForm.formKey; };
            SwitchTransitProvinceDialog.prototype.getIdProperty = function () { return Case.SwitchTransitProvinceRow.idProperty; };
            SwitchTransitProvinceDialog.prototype.getLocalTextPrefix = function () { return Case.SwitchTransitProvinceRow.localTextPrefix; };
            SwitchTransitProvinceDialog.prototype.getNameProperty = function () { return Case.SwitchTransitProvinceRow.nameProperty; };
            SwitchTransitProvinceDialog.prototype.getService = function () { return Case.SwitchTransitProvinceService.baseUrl; };
            SwitchTransitProvinceDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadOnly(this.form.ProvinceId, false);
            };
            SwitchTransitProvinceDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SwitchTransitProvinceDialog);
            return SwitchTransitProvinceDialog;
        }(Serenity.EntityDialog));
        Case.SwitchTransitProvinceDialog = SwitchTransitProvinceDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceCompanySoftwareDialog = (function (_super) {
            __extends(ProvinceCompanySoftwareDialog, _super);
            function ProvinceCompanySoftwareDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ProvinceCompanySoftwareForm(this.idPrefix);
            }
            ProvinceCompanySoftwareDialog.prototype.getFormKey = function () { return Case.ProvinceCompanySoftwareForm.formKey; };
            ProvinceCompanySoftwareDialog.prototype.getIdProperty = function () { return Case.ProvinceCompanySoftwareRow.idProperty; };
            ProvinceCompanySoftwareDialog.prototype.getLocalTextPrefix = function () { return Case.ProvinceCompanySoftwareRow.localTextPrefix; };
            ProvinceCompanySoftwareDialog.prototype.getService = function () { return Case.ProvinceCompanySoftwareService.baseUrl; };
            ProvinceCompanySoftwareDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ProvinceCompanySoftwareDialog);
            return ProvinceCompanySoftwareDialog;
        }(Serenity.EntityDialog));
        Case.ProvinceCompanySoftwareDialog = ProvinceCompanySoftwareDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramDialog = (function (_super) {
            __extends(ProvinceProgramDialog, _super);
            function ProvinceProgramDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ProvinceProgramForm(this.idPrefix);
            }
            ProvinceProgramDialog.prototype.getFormKey = function () { return Case.ProvinceProgramForm.formKey; };
            ProvinceProgramDialog.prototype.getIdProperty = function () { return Case.ProvinceProgramRow.idProperty; };
            ProvinceProgramDialog.prototype.getLocalTextPrefix = function () { return Case.ProvinceProgramRow.localTextPrefix; };
            ProvinceProgramDialog.prototype.getService = function () { return Case.ProvinceProgramService.baseUrl; };
            ProvinceProgramDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ProvinceProgramDialog);
            return ProvinceProgramDialog;
        }(Serenity.EntityDialog));
        Case.ProvinceProgramDialog = ProvinceProgramDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramGrid = (function (_super) {
            __extends(ProvinceProgramGrid, _super);
            function ProvinceProgramGrid(container) {
                _super.call(this, container);
            }
            ProvinceProgramGrid.prototype.getColumnsKey = function () { return 'Case.ProvinceProgram'; };
            ProvinceProgramGrid.prototype.getDialogType = function () { return Case.ProvinceProgramDialog; };
            ProvinceProgramGrid.prototype.getIdProperty = function () { return Case.ProvinceProgramRow.idProperty; };
            ProvinceProgramGrid.prototype.getLocalTextPrefix = function () { return Case.ProvinceProgramRow.localTextPrefix; };
            ProvinceProgramGrid.prototype.getService = function () { return Case.ProvinceProgramService.baseUrl; };
            ProvinceProgramGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage'),
                        new Slick.Aggregators.Sum('RecoverableLeakage'),
                        new Slick.Aggregators.Sum('Recovered'),
                        new Slick.Aggregators.Sum('Program')
                    ]
                });
                return grid;
            };
            ProvinceProgramGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ProvinceProgramGrid.prototype.usePager = function () {
                return false;
            };
            ProvinceProgramGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.ProvinceProgramService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        }
                        var Year = document.getElementById("select2-chosen-2").innerHTML;
                        if (Year == null) {
                            Year = "";
                        }
                        window.location.href = "../Common/ProvinceProgramPrint?Year=" + Year + "&Province=" + Province;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ProvinceProgramGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceProgramGrid);
            return ProvinceProgramGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceProgramGrid = ProvinceProgramGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramLogDialog = (function (_super) {
            __extends(ProvinceProgramLogDialog, _super);
            function ProvinceProgramLogDialog() {
                _super.apply(this, arguments);
                this.form = new Case.ProvinceProgramLogForm(this.idPrefix);
            }
            ProvinceProgramLogDialog.prototype.getFormKey = function () { return Case.ProvinceProgramLogForm.formKey; };
            ProvinceProgramLogDialog.prototype.getIdProperty = function () { return Case.ProvinceProgramLogRow.idProperty; };
            ProvinceProgramLogDialog.prototype.getLocalTextPrefix = function () { return Case.ProvinceProgramLogRow.localTextPrefix; };
            ProvinceProgramLogDialog.prototype.getService = function () { return Case.ProvinceProgramLogService.baseUrl; };
            ProvinceProgramLogDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ProvinceProgramLogDialog);
            return ProvinceProgramLogDialog;
        }(Serenity.EntityDialog));
        Case.ProvinceProgramLogDialog = ProvinceProgramLogDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramLogGrid = (function (_super) {
            __extends(ProvinceProgramLogGrid, _super);
            function ProvinceProgramLogGrid(container) {
                _super.call(this, container);
            }
            ProvinceProgramLogGrid.prototype.getColumnsKey = function () { return 'Case.ProvinceProgramLog'; };
            ProvinceProgramLogGrid.prototype.getDialogType = function () { return Case.ProvinceProgramLogDialog; };
            ProvinceProgramLogGrid.prototype.getIdProperty = function () { return Case.ProvinceProgramLogRow.idProperty; };
            ProvinceProgramLogGrid.prototype.getLocalTextPrefix = function () { return Case.ProvinceProgramLogRow.localTextPrefix; };
            ProvinceProgramLogGrid.prototype.getService = function () { return Case.ProvinceProgramLogService.baseUrl; };
            ProvinceProgramLogGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ProvinceProgramLogGrid);
            return ProvinceProgramLogGrid;
        }(Serenity.EntityGrid));
        Case.ProvinceProgramLogGrid = ProvinceProgramLogGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RepeatTermDialog = (function (_super) {
            __extends(RepeatTermDialog, _super);
            function RepeatTermDialog() {
                _super.apply(this, arguments);
                this.form = new Case.RepeatTermForm(this.idPrefix);
            }
            RepeatTermDialog.prototype.getFormKey = function () { return Case.RepeatTermForm.formKey; };
            RepeatTermDialog.prototype.getIdProperty = function () { return Case.RepeatTermRow.idProperty; };
            RepeatTermDialog.prototype.getLocalTextPrefix = function () { return Case.RepeatTermRow.localTextPrefix; };
            RepeatTermDialog.prototype.getNameProperty = function () { return Case.RepeatTermRow.nameProperty; };
            RepeatTermDialog.prototype.getService = function () { return Case.RepeatTermService.baseUrl; };
            RepeatTermDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], RepeatTermDialog);
            return RepeatTermDialog;
        }(Serenity.EntityDialog));
        Case.RepeatTermDialog = RepeatTermDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RepeatTermGrid = (function (_super) {
            __extends(RepeatTermGrid, _super);
            function RepeatTermGrid(container) {
                _super.call(this, container);
            }
            RepeatTermGrid.prototype.getColumnsKey = function () { return 'Case.RepeatTerm'; };
            RepeatTermGrid.prototype.getDialogType = function () { return Case.RepeatTermDialog; };
            RepeatTermGrid.prototype.getIdProperty = function () { return Case.RepeatTermRow.idProperty; };
            RepeatTermGrid.prototype.getLocalTextPrefix = function () { return Case.RepeatTermRow.localTextPrefix; };
            RepeatTermGrid.prototype.getService = function () { return Case.RepeatTermService.baseUrl; };
            RepeatTermGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], RepeatTermGrid);
            return RepeatTermGrid;
        }(Serenity.EntityGrid));
        Case.RepeatTermGrid = RepeatTermGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RiskLevelDialog = (function (_super) {
            __extends(RiskLevelDialog, _super);
            function RiskLevelDialog() {
                _super.apply(this, arguments);
                this.form = new Case.RiskLevelForm(this.idPrefix);
            }
            RiskLevelDialog.prototype.getFormKey = function () { return Case.RiskLevelForm.formKey; };
            RiskLevelDialog.prototype.getIdProperty = function () { return Case.RiskLevelRow.idProperty; };
            RiskLevelDialog.prototype.getLocalTextPrefix = function () { return Case.RiskLevelRow.localTextPrefix; };
            RiskLevelDialog.prototype.getNameProperty = function () { return Case.RiskLevelRow.nameProperty; };
            RiskLevelDialog.prototype.getService = function () { return Case.RiskLevelService.baseUrl; };
            RiskLevelDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], RiskLevelDialog);
            return RiskLevelDialog;
        }(Serenity.EntityDialog));
        Case.RiskLevelDialog = RiskLevelDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RiskLevelGrid = (function (_super) {
            __extends(RiskLevelGrid, _super);
            function RiskLevelGrid(container) {
                _super.call(this, container);
            }
            RiskLevelGrid.prototype.getColumnsKey = function () { return 'Case.RiskLevel'; };
            RiskLevelGrid.prototype.getDialogType = function () { return Case.RiskLevelDialog; };
            RiskLevelGrid.prototype.getIdProperty = function () { return Case.RiskLevelRow.idProperty; };
            RiskLevelGrid.prototype.getLocalTextPrefix = function () { return Case.RiskLevelRow.localTextPrefix; };
            RiskLevelGrid.prototype.getService = function () { return Case.RiskLevelService.baseUrl; };
            RiskLevelGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], RiskLevelGrid);
            return RiskLevelGrid;
        }(Serenity.EntityGrid));
        Case.RiskLevelGrid = RiskLevelGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SMSLogDialog = (function (_super) {
            __extends(SMSLogDialog, _super);
            function SMSLogDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SMSLogForm(this.idPrefix);
            }
            SMSLogDialog.prototype.getFormKey = function () { return Case.SMSLogForm.formKey; };
            SMSLogDialog.prototype.getIdProperty = function () { return Case.SMSLogRow.idProperty; };
            SMSLogDialog.prototype.getLocalTextPrefix = function () { return Case.SMSLogRow.localTextPrefix; };
            SMSLogDialog.prototype.getNameProperty = function () { return Case.SMSLogRow.nameProperty; };
            SMSLogDialog.prototype.getService = function () { return Case.SMSLogService.baseUrl; };
            SMSLogDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SMSLogDialog);
            return SMSLogDialog;
        }(Serenity.EntityDialog));
        Case.SMSLogDialog = SMSLogDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SMSLogGrid = (function (_super) {
            __extends(SMSLogGrid, _super);
            function SMSLogGrid(container) {
                _super.call(this, container);
            }
            SMSLogGrid.prototype.getColumnsKey = function () { return 'Case.SMSLog'; };
            SMSLogGrid.prototype.getDialogType = function () { return Case.SMSLogDialog; };
            SMSLogGrid.prototype.getIdProperty = function () { return Case.SMSLogRow.idProperty; };
            SMSLogGrid.prototype.getLocalTextPrefix = function () { return Case.SMSLogRow.localTextPrefix; };
            SMSLogGrid.prototype.getService = function () { return Case.SMSLogService.baseUrl; };
            SMSLogGrid.prototype.PersiaNumber = function (value) {
                var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
                //console.log(value);
                var chars = value.split('');
                for (var i = 0; i < chars.length; i++) {
                    if (/\d/.test(chars[i])) {
                        chars[i] = arabicNumbers[chars[i]];
                    }
                }
                return chars.join('');
            };
            SMSLogGrid.prototype.getColumns = function () {
                var _this = this;
                var columns = _super.prototype.getColumns.call(this);
                var fld = Case.SMSLogRow.Fields;
                Q.first(columns, function (x) { return x.field == fld.ActivityRequestId; }).format =
                    function (ctx) { return ("<a href=\"javascript:;\" class=\"ActivityRequest-link\">" + Q.htmlEncode(_this.PersiaNumber(ctx.value.toString())) + "</a>"); };
                return columns;
            };
            SMSLogGrid.prototype.onClick = function (e, row, cell) {
                // let base grid handle clicks for its edit links
                _super.prototype.onClick.call(this, e, row, cell);
                // if base grid already handled, we shouldn"t handle it again
                if (e.isDefaultPrevented()) {
                    return;
                }
                // get reference to current item
                var item = this.itemAt(row);
                // get reference to clicked element
                var target = $(e.target);
                if (target.hasClass("ActivityRequest-link")) {
                    e.preventDefault();
                    var ActivityRequest = Q.first(Case.ActivityRequestPenddingRow.getLookup().items, function (x) { return x.Id == item.ActivityRequestId; });
                    new Case.ActivityRequestPenddingDialog().loadByIdAndOpenDialog(ActivityRequest.Id);
                }
            };
            SMSLogGrid.prototype.getButtons = function () {
                var buttons = _super.prototype.getButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            SMSLogGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SMSLogGrid);
            return SMSLogGrid;
        }(Serenity.EntityGrid));
        Case.SMSLogGrid = SMSLogGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SoftwareDialog = (function (_super) {
            __extends(SoftwareDialog, _super);
            function SoftwareDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SoftwareForm(this.idPrefix);
            }
            SoftwareDialog.prototype.getFormKey = function () { return Case.SoftwareForm.formKey; };
            SoftwareDialog.prototype.getIdProperty = function () { return Case.SoftwareRow.idProperty; };
            SoftwareDialog.prototype.getLocalTextPrefix = function () { return Case.SoftwareRow.localTextPrefix; };
            SoftwareDialog.prototype.getNameProperty = function () { return Case.SoftwareRow.nameProperty; };
            SoftwareDialog.prototype.getService = function () { return Case.SoftwareService.baseUrl; };
            SoftwareDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SoftwareDialog);
            return SoftwareDialog;
        }(Serenity.EntityDialog));
        Case.SoftwareDialog = SoftwareDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SoftwareGrid = (function (_super) {
            __extends(SoftwareGrid, _super);
            function SoftwareGrid(container) {
                _super.call(this, container);
            }
            SoftwareGrid.prototype.getColumnsKey = function () { return 'Case.Software'; };
            SoftwareGrid.prototype.getDialogType = function () { return Case.SoftwareDialog; };
            SoftwareGrid.prototype.getIdProperty = function () { return Case.SoftwareRow.idProperty; };
            SoftwareGrid.prototype.getLocalTextPrefix = function () { return Case.SoftwareRow.localTextPrefix; };
            SoftwareGrid.prototype.getService = function () { return Case.SoftwareService.baseUrl; };
            SoftwareGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SoftwareGrid);
            return SoftwareGrid;
        }(Serenity.EntityGrid));
        Case.SoftwareGrid = SoftwareGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDialog = (function (_super) {
            __extends(SwitchDialog, _super);
            function SwitchDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SwitchForm(this.idPrefix);
            }
            SwitchDialog.prototype.getFormKey = function () { return Case.SwitchForm.formKey; };
            SwitchDialog.prototype.getIdProperty = function () { return Case.SwitchRow.idProperty; };
            SwitchDialog.prototype.getLocalTextPrefix = function () { return Case.SwitchRow.localTextPrefix; };
            SwitchDialog.prototype.getNameProperty = function () { return Case.SwitchRow.nameProperty; };
            SwitchDialog.prototype.getService = function () { return Case.SwitchService.baseUrl; };
            SwitchDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SwitchDialog);
            return SwitchDialog;
        }(Serenity.EntityDialog));
        Case.SwitchDialog = SwitchDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchGrid = (function (_super) {
            __extends(SwitchGrid, _super);
            function SwitchGrid(container) {
                _super.call(this, container);
            }
            SwitchGrid.prototype.getColumnsKey = function () { return 'Case.Switch'; };
            SwitchGrid.prototype.getDialogType = function () { return Case.SwitchDialog; };
            SwitchGrid.prototype.getIdProperty = function () { return Case.SwitchRow.idProperty; };
            SwitchGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchRow.localTextPrefix; };
            SwitchGrid.prototype.getService = function () { return Case.SwitchService.baseUrl; };
            SwitchGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.SwitchService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                return buttons;
            };
            SwitchGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SwitchGrid);
            return SwitchGrid;
        }(Serenity.EntityGrid));
        Case.SwitchGrid = SwitchGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamDialog = (function (_super) {
            __extends(SwitchDslamDialog, _super);
            function SwitchDslamDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SwitchDslamForm(this.idPrefix);
            }
            SwitchDslamDialog.prototype.getFormKey = function () { return Case.SwitchDslamForm.formKey; };
            SwitchDslamDialog.prototype.getIdProperty = function () { return Case.SwitchDslamRow.idProperty; };
            SwitchDslamDialog.prototype.getLocalTextPrefix = function () { return Case.SwitchDslamRow.localTextPrefix; };
            SwitchDslamDialog.prototype.getNameProperty = function () { return Case.SwitchDslamRow.nameProperty; };
            SwitchDslamDialog.prototype.getService = function () { return Case.SwitchDslamService.baseUrl; };
            SwitchDslamDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SwitchDslamDialog);
            return SwitchDslamDialog;
        }(Serenity.EntityDialog));
        Case.SwitchDslamDialog = SwitchDslamDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamGrid = (function (_super) {
            __extends(SwitchDslamGrid, _super);
            function SwitchDslamGrid(container) {
                _super.call(this, container);
            }
            SwitchDslamGrid.prototype.getColumnsKey = function () { return 'Case.SwitchDslam'; };
            SwitchDslamGrid.prototype.getDialogType = function () { return Case.SwitchDslamDialog; };
            SwitchDslamGrid.prototype.getIdProperty = function () { return Case.SwitchDslamRow.idProperty; };
            SwitchDslamGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchDslamRow.localTextPrefix; };
            SwitchDslamGrid.prototype.getService = function () { return Case.SwitchDslamService.baseUrl; };
            SwitchDslamGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.SwitchDslamService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                return buttons;
            };
            SwitchDslamGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SwitchDslamGrid);
            return SwitchDslamGrid;
        }(Serenity.EntityGrid));
        Case.SwitchDslamGrid = SwitchDslamGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamProvinceDialog = (function (_super) {
            __extends(SwitchDslamProvinceDialog, _super);
            function SwitchDslamProvinceDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SwitchDslamProvinceForm(this.idPrefix);
            }
            SwitchDslamProvinceDialog.prototype.getFormKey = function () { return Case.SwitchDslamProvinceForm.formKey; };
            SwitchDslamProvinceDialog.prototype.getIdProperty = function () { return Case.SwitchDslamProvinceRow.idProperty; };
            SwitchDslamProvinceDialog.prototype.getLocalTextPrefix = function () { return Case.SwitchDslamProvinceRow.localTextPrefix; };
            SwitchDslamProvinceDialog.prototype.getService = function () { return Case.SwitchDslamProvinceService.baseUrl; };
            SwitchDslamProvinceDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SwitchDslamProvinceDialog);
            return SwitchDslamProvinceDialog;
        }(Serenity.EntityDialog));
        Case.SwitchDslamProvinceDialog = SwitchDslamProvinceDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitDialog = (function (_super) {
            __extends(SwitchTransitDialog, _super);
            function SwitchTransitDialog() {
                _super.apply(this, arguments);
                this.form = new Case.SwitchTransitForm(this.idPrefix);
            }
            SwitchTransitDialog.prototype.getFormKey = function () { return Case.SwitchTransitForm.formKey; };
            SwitchTransitDialog.prototype.getIdProperty = function () { return Case.SwitchTransitRow.idProperty; };
            SwitchTransitDialog.prototype.getLocalTextPrefix = function () { return Case.SwitchTransitRow.localTextPrefix; };
            SwitchTransitDialog.prototype.getNameProperty = function () { return Case.SwitchTransitRow.nameProperty; };
            SwitchTransitDialog.prototype.getService = function () { return Case.SwitchTransitService.baseUrl; };
            SwitchTransitDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SwitchTransitDialog);
            return SwitchTransitDialog;
        }(Serenity.EntityDialog));
        Case.SwitchTransitDialog = SwitchTransitDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitGrid = (function (_super) {
            __extends(SwitchTransitGrid, _super);
            function SwitchTransitGrid(container) {
                _super.call(this, container);
            }
            SwitchTransitGrid.prototype.getColumnsKey = function () { return 'Case.SwitchTransit'; };
            SwitchTransitGrid.prototype.getDialogType = function () { return Case.SwitchTransitDialog; };
            SwitchTransitGrid.prototype.getIdProperty = function () { return Case.SwitchTransitRow.idProperty; };
            SwitchTransitGrid.prototype.getLocalTextPrefix = function () { return Case.SwitchTransitRow.localTextPrefix; };
            SwitchTransitGrid.prototype.getService = function () { return Case.SwitchTransitService.baseUrl; };
            SwitchTransitGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: Case.SwitchTransitService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                return buttons;
            };
            SwitchTransitGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SwitchTransitGrid);
            return SwitchTransitGrid;
        }(Serenity.EntityGrid));
        Case.SwitchTransitGrid = SwitchTransitGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var YearDialog = (function (_super) {
            __extends(YearDialog, _super);
            function YearDialog() {
                _super.apply(this, arguments);
                this.form = new Case.YearForm(this.idPrefix);
            }
            YearDialog.prototype.getFormKey = function () { return Case.YearForm.formKey; };
            YearDialog.prototype.getIdProperty = function () { return Case.YearRow.idProperty; };
            YearDialog.prototype.getLocalTextPrefix = function () { return Case.YearRow.localTextPrefix; };
            YearDialog.prototype.getNameProperty = function () { return Case.YearRow.nameProperty; };
            YearDialog.prototype.getService = function () { return Case.YearService.baseUrl; };
            YearDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], YearDialog);
            return YearDialog;
        }(Serenity.EntityDialog));
        Case.YearDialog = YearDialog;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var YearGrid = (function (_super) {
            __extends(YearGrid, _super);
            function YearGrid(container) {
                _super.call(this, container);
            }
            YearGrid.prototype.getColumnsKey = function () { return 'Case.Year'; };
            YearGrid.prototype.getDialogType = function () { return Case.YearDialog; };
            YearGrid.prototype.getIdProperty = function () { return Case.YearRow.idProperty; };
            YearGrid.prototype.getLocalTextPrefix = function () { return Case.YearRow.localTextPrefix; };
            YearGrid.prototype.getService = function () { return Case.YearService.baseUrl; };
            YearGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], YearGrid);
            return YearGrid;
        }(Serenity.EntityGrid));
        Case.YearGrid = YearGrid;
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var ScriptInitialization;
    (function (ScriptInitialization) {
        Q.Config.responsiveDialogs = true;
        Q.Config.rootNamespaces.push('CaseManagement');
    })(ScriptInitialization = CaseManagement.ScriptInitialization || (CaseManagement.ScriptInitialization = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var DashboardIndex = (function (_super) {
            __extends(DashboardIndex, _super);
            function DashboardIndex() {
                _super.apply(this, arguments);
            }
            DashboardIndex.ProvinceProgram95 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            DashboardIndex = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], DashboardIndex);
            return DashboardIndex;
        }(Serenity.TemplatedDialog));
        Common.DashboardIndex = DashboardIndex;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var DashboardUser;
        (function (DashboardUser) {
            function initializePage() {
                Messages();
            }
            DashboardUser.initializePage = initializePage;
            function Messages() {
                var Unreadcounter = 0;
                var UnReadMessages;
                $.ajaxSetup({ async: false });
                var InboxResult = CaseManagement.Messaging.VwMessagesService.List({ "Sort": ["InsertedDate DESC"] }, function (response) {
                    //console.log("succesfully called Inbox");
                    // console.log(response.TotalCount);  
                    UnReadMessages = new Array(5);
                    for (j = 0; j < 5; j++) {
                        UnReadMessages[j] = new Array(3);
                    }
                    for (var InboxCount = 0; InboxCount < response.TotalCount; InboxCount++) {
                        if (response.Entities[InboxCount].Seen == null && Unreadcounter != 5) {
                            UnReadMessages[Unreadcounter][0] = response.Entities[InboxCount].Id;
                            UnReadMessages[Unreadcounter][1] = response.Entities[InboxCount].SenderName;
                            UnReadMessages[Unreadcounter][2] = response.Entities[InboxCount].Subject;
                            Unreadcounter++;
                        }
                    }
                    var Messages = "شما تعداد " + PersiaNumber(Unreadcounter.toString()) + " پیام خوانده نشده دارید :  \n\n";
                    if (UnReadMessages[0][0] != null) {
                        Messages = Messages + UnReadMessages[0][1] + " : " + UnReadMessages[0][2] + "  \n";
                        $("#first_message_SenderName").text("فرستنده : " + UnReadMessages[0][1]);
                        $("#first_message_subject").text("موضوع : " + UnReadMessages[0][2]);
                    }
                    else {
                        document.getElementById("first_message_SenderName").classList.remove('label-default');
                        document.getElementById("first_message_SenderName").classList.add('label-danger');
                        $("#first_message_SenderName").text('پیام جدیدی در لیست پیام های دریافت شده وجود ندارد.');
                        $("#first_message_subject").text(' ');
                    }
                    if (UnReadMessages[1][0] != null) {
                        Messages = Messages + UnReadMessages[1][1] + " : " + UnReadMessages[1][2] + "  \n";
                        $("#second_message_SenderName").text("فرستنده : " + UnReadMessages[1][1]);
                        $("#second_message_subject").text("موضوع : " + UnReadMessages[1][2]);
                    }
                    else {
                        document.getElementById("second_message_SenderName").classList.remove('label-success');
                        $("#second_message_SenderName").text(' ');
                        $("#second_message_subject").text(' ');
                    }
                    if (UnReadMessages[2][0] != null) {
                        Messages = Messages + UnReadMessages[2][1] + " : " + UnReadMessages[2][2] + "  \n";
                        $("#third_message_SenderName").text("فرستنده : " + UnReadMessages[2][1]);
                        $("#third_message_subject").text("موضوع : " + UnReadMessages[2][2]);
                    }
                    else {
                        document.getElementById("third_message_SenderName").classList.remove('label-default');
                        $("#third_message_SenderName").text(' ');
                        $("#third_message_subject").text(' ');
                    }
                    if (UnReadMessages[3][0] != null) {
                        Messages = Messages + UnReadMessages[3][1] + " : " + UnReadMessages[3][2] + "  \n";
                        $("#fourth_message_SenderName").text("فرستنده : " + UnReadMessages[3][1]);
                        $("#fourth_message_subject").text("موضوع : " + UnReadMessages[3][2]);
                    }
                    else {
                        document.getElementById("fourth_message_SenderName").classList.remove('label-success');
                        $("#fourth_message_SenderName").text(' ');
                        $("#fourth_message_subject").text(' ');
                    }
                    if (UnReadMessages[4][0] != null) {
                        Messages = Messages + UnReadMessages[4][1] + " : " + UnReadMessages[4][2] + "  \n";
                        $("#fifth_message_SenderName").text("فرستنده : " + UnReadMessages[4][1]);
                        $("#fifth_message_subject").text("موضوع : " + UnReadMessages[4][2]);
                    }
                    else {
                        document.getElementById("fifth_message_SenderName").classList.remove('label-default');
                        $("#fifth_message_SenderName").text(' ');
                        $("#fifth_message_subject").text(' ');
                    }
                    if (Unreadcounter != 0) {
                        Q.information(Messages, function () {
                            Q.notifySuccess("اطلاع رسانی گردید.");
                        });
                    }
                });
            }
            function PersiaNumber(value) {
                var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
                var chars = value.split('');
                for (var i = 0; i < chars.length; i++) {
                    if (/\d/.test(chars[i])) {
                        chars[i] = arabicNumbers[chars[i]];
                    }
                }
                return chars.join('');
            }
        })(DashboardUser = Common.DashboardUser || (Common.DashboardUser = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var BasicProgressDialog = (function (_super) {
        __extends(BasicProgressDialog, _super);
        function BasicProgressDialog() {
            var _this = this;
            _super.call(this);
            this.byId('ProgressBar').progressbar({
                max: 100,
                value: 0,
                change: function (e, v) {
                    _this.byId('ProgressLabel').text(_this.value + ' / ' + _this.max);
                }
            });
        }
        Object.defineProperty(BasicProgressDialog.prototype, "max", {
            get: function () {
                return this.byId('ProgressBar').progressbar().progressbar('option', 'max');
            },
            set: function (value) {
                this.byId('ProgressBar').progressbar().progressbar('option', 'max', value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(BasicProgressDialog.prototype, "value", {
            get: function () {
                return this.byId('ProgressBar').progressbar('value');
            },
            set: function (value) {
                this.byId('ProgressBar').progressbar().progressbar('value', value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(BasicProgressDialog.prototype, "title", {
            get: function () {
                return this.element.dialog().dialog('option', 'title');
            },
            set: function (value) {
                this.element.dialog().dialog('option', 'title', value);
            },
            enumerable: true,
            configurable: true
        });
        BasicProgressDialog.prototype.getDialogOptions = function () {
            var _this = this;
            var opt = _super.prototype.getDialogOptions.call(this);
            opt.title = Q.text('Site.BasicProgressDialog.PleaseWait');
            opt.width = 600;
            opt.buttons = [{
                    text: Q.text('Dialogs.CancelButton'),
                    click: function () {
                        _this.cancelled = true;
                        _this.element.closest('.ui-dialog')
                            .find('.ui-dialog-buttonpane .ui-button')
                            .attr('disabled', 'disabled')
                            .css('opacity', '0.5');
                        _this.element.dialog('option', 'title', Q.trimToNull(_this.cancelTitle) ||
                            Q.text('Site.BasicProgressDialog.CancelTitle'));
                    }
                }];
            return opt;
        };
        BasicProgressDialog.prototype.initDialog = function () {
            _super.prototype.initDialog.call(this);
            this.element.closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
        };
        BasicProgressDialog.prototype.getTemplate = function () {
            return ("<div class='s-DialogContent s-BasicProgressDialogContent'>" +
                "<div id='~_StatusText' class='status-text' ></div>" +
                "<div id='~_ProgressBar' class='progress-bar'>" +
                "<div id='~_ProgressLabel' class='progress-label' ></div>" +
                "</div>" +
                "</div>");
        };
        return BasicProgressDialog;
    }(Serenity.TemplatedDialog));
    CaseManagement.BasicProgressDialog = BasicProgressDialog;
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var BulkServiceAction = (function () {
            function BulkServiceAction() {
            }
            BulkServiceAction.prototype.createProgressDialog = function () {
                this.progressDialog = new CaseManagement.BasicProgressDialog();
                this.progressDialog.dialogOpen();
                this.progressDialog.max = this.keys.length;
                this.progressDialog.value = 0;
            };
            BulkServiceAction.prototype.getConfirmationFormat = function () {
                return Q.text('Site.BulkServiceAction.ConfirmationFormat');
            };
            BulkServiceAction.prototype.getConfirmationMessage = function (targetCount) {
                return Q.format(this.getConfirmationFormat(), targetCount);
            };
            BulkServiceAction.prototype.confirm = function (targetCount, action) {
                Q.confirm(this.getConfirmationMessage(targetCount), action);
            };
            BulkServiceAction.prototype.getNothingToProcessMessage = function () {
                return Q.text('Site.BulkServiceAction.NothingToProcess');
            };
            BulkServiceAction.prototype.nothingToProcess = function () {
                Q.notifyError(this.getNothingToProcessMessage());
            };
            BulkServiceAction.prototype.getParallelRequests = function () {
                return 1;
            };
            BulkServiceAction.prototype.getBatchSize = function () {
                return 1;
            };
            BulkServiceAction.prototype.startParallelExecution = function () {
                this.createProgressDialog();
                this.successCount = 0;
                this.errorCount = 0;
                this.pendingRequests = 0;
                this.completedRequests = 0;
                this.errorCount = 0;
                this.errorByKey = {};
                this.queue = this.keys.slice();
                this.queueIndex = 0;
                var parallelRequests = this.getParallelRequests();
                while (parallelRequests-- > 0) {
                    this.executeNextBatch();
                }
            };
            BulkServiceAction.prototype.serviceCallCleanup = function () {
                this.pendingRequests--;
                this.completedRequests++;
                var title = Q.text((this.progressDialog.cancelled ?
                    'Site.BasicProgressDialog.CancelTitle' : 'Site.BasicProgressDialog.PleaseWait'));
                title += ' (';
                if (this.successCount > 0) {
                    title += Q.format(Q.text('Site.BulkServiceAction.SuccessCount'), this.successCount);
                }
                if (this.errorCount > 0) {
                    if (this.successCount > 0) {
                        title += ', ';
                    }
                    title += Q.format(Q.text('Site.BulkServiceAction.ErrorCount'), this.errorCount);
                }
                this.progressDialog.title = title + ')';
                this.progressDialog.value = this.successCount + this.errorCount;
                if (!this.progressDialog.cancelled && this.progressDialog.value < this.keys.length) {
                    this.executeNextBatch();
                }
                else if (this.pendingRequests === 0) {
                    this.progressDialog.dialogClose();
                    this.showResults();
                    if (this.done) {
                        this.done();
                        this.done = null;
                    }
                }
            };
            BulkServiceAction.prototype.executeForBatch = function (batch) {
            };
            BulkServiceAction.prototype.executeNextBatch = function () {
                var batchSize = this.getBatchSize();
                var batch = [];
                while (true) {
                    if (batch.length >= batchSize) {
                        break;
                    }
                    if (this.queueIndex >= this.queue.length) {
                        break;
                    }
                    batch.push(this.queue[this.queueIndex++]);
                }
                if (batch.length > 0) {
                    this.pendingRequests++;
                    this.executeForBatch(batch);
                }
            };
            BulkServiceAction.prototype.getAllHadErrorsFormat = function () {
                return Q.text('Site.BulkServiceAction.AllHadErrorsFormat');
            };
            BulkServiceAction.prototype.showAllHadErrors = function () {
                Q.notifyError(Q.format(this.getAllHadErrorsFormat(), this.errorCount));
            };
            BulkServiceAction.prototype.getSomeHadErrorsFormat = function () {
                return Q.text('Site.BulkServiceAction.SomeHadErrorsFormat');
            };
            BulkServiceAction.prototype.showSomeHadErrors = function () {
                Q.notifyWarning(Q.format(this.getSomeHadErrorsFormat(), this.successCount, this.errorCount));
            };
            BulkServiceAction.prototype.getAllSuccessFormat = function () {
                return Q.text('Site.BulkServiceAction.AllSuccessFormat');
            };
            BulkServiceAction.prototype.showAllSuccess = function () {
                Q.notifySuccess(Q.format(this.getAllSuccessFormat(), this.successCount));
            };
            BulkServiceAction.prototype.showResults = function () {
                if (this.errorCount === 0 && this.successCount === 0) {
                    this.nothingToProcess();
                    return;
                }
                if (this.errorCount > 0 && this.successCount === 0) {
                    this.showAllHadErrors();
                    return;
                }
                if (this.errorCount > 0) {
                    this.showSomeHadErrors();
                    return;
                }
                this.showAllSuccess();
            };
            BulkServiceAction.prototype.execute = function (keys) {
                var _this = this;
                this.keys = keys;
                if (this.keys.length === 0) {
                    this.nothingToProcess();
                    return;
                }
                this.confirm(this.keys.length, function () { return _this.startParallelExecution(); });
            };
            BulkServiceAction.prototype.get_successCount = function () {
                return this.successCount;
            };
            BulkServiceAction.prototype.set_successCount = function (value) {
                this.successCount = value;
            };
            BulkServiceAction.prototype.get_errorCount = function () {
                return this.errorCount;
            };
            BulkServiceAction.prototype.set_errorCount = function (value) {
                this.errorCount = value;
            };
            return BulkServiceAction;
        }());
        Common.BulkServiceAction = BulkServiceAction;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var DialogUtils;
    (function (DialogUtils) {
        function pendingChangesConfirmation(element, hasPendingChanges) {
            element.bind('dialogbeforeclose', function (e) {
                if (!Serenity.WX.hasOriginalEvent(e) || !hasPendingChanges()) {
                    return;
                }
                e.preventDefault();
                Q.confirm('You have pending changes. Save them?', function () { return element.find('div.save-and-close-button').click(); }, {
                    onNo: function () {
                        element.dialog().dialog('close');
                    }
                });
            });
        }
        DialogUtils.pendingChangesConfirmation = pendingChangesConfirmation;
    })(DialogUtils = CaseManagement.DialogUtils || (CaseManagement.DialogUtils = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var ExcelExportHelper;
        (function (ExcelExportHelper) {
            function createToolButton(options) {
                return {
                    hint: Q.coalesce(options.title, 'Excel'),
                    title: Q.coalesce(options.hint, ''),
                    cssClass: 'export-xlsx-button',
                    onClick: function () {
                        if (!options.onViewSubmit()) {
                            return;
                        }
                        var grid = options.grid;
                        var request = Q.deepClone(grid.getView().params);
                        request.Take = 0;
                        request.Skip = 0;
                        var sortBy = grid.getView().sortBy;
                        if (sortBy) {
                            request.Sort = sortBy;
                        }
                        request.IncludeColumns = [];
                        var columns = grid.getGrid().getColumns();
                        for (var _i = 0, columns_1 = columns; _i < columns_1.length; _i++) {
                            var column = columns_1[_i];
                            request.IncludeColumns.push(column.id || column.field);
                        }
                        Q.postToService({ service: options.service, request: request, target: '_blank' });
                    },
                    separator: options.separator
                };
            }
            ExcelExportHelper.createToolButton = createToolButton;
        })(ExcelExportHelper = Common.ExcelExportHelper || (Common.ExcelExportHelper = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var LanguageList;
    (function (LanguageList) {
        function getValue() {
            var result = [];
            for (var _i = 0, _a = CaseManagement.Administration.LanguageRow.getLookup().items; _i < _a.length; _i++) {
                var k = _a[_i];
                if (k.LanguageId !== 'en') {
                    result.push([k.Id.toString(), k.LanguageName]);
                }
            }
            return result;
        }
        LanguageList.getValue = getValue;
    })(LanguageList = CaseManagement.LanguageList || (CaseManagement.LanguageList = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var ReportHelper;
        (function (ReportHelper) {
            function createToolButton(options) {
                return {
                    title: Q.coalesce(options.title, 'Report'),
                    cssClass: Q.coalesce(options.cssClass, 'print-button'),
                    icon: options.icon,
                    onClick: function () {
                        Q.postToUrl({
                            url: '~/Report/' + (options.download ? 'Download' : 'Render'),
                            params: {
                                key: options.reportKey,
                                ext: Q.coalesce(options.extension, 'pdf'),
                                opt: (options.getParams == null ? '' : $.toJSON(options.getParams()))
                            },
                            target: Q.coalesce(options.target, '_blank')
                        });
                    }
                };
            }
            ReportHelper.createToolButton = createToolButton;
        })(ReportHelper = Common.ReportHelper || (Common.ReportHelper = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        (function (ActionLog) {
            ActionLog[ActionLog["View"] = 1] = "View";
            ActionLog[ActionLog["Insert"] = 2] = "Insert";
            ActionLog[ActionLog["Update"] = 3] = "Update";
            ActionLog[ActionLog["Delete"] = 4] = "Delete";
            ActionLog[ActionLog["Login"] = 5] = "Login";
            ActionLog[ActionLog["Logout"] = 6] = "Logout";
        })(Administration.ActionLog || (Administration.ActionLog = {}));
        var ActionLog = Administration.ActionLog;
        Serenity.Decorators.registerEnum(ActionLog, 'Administration.ActionLog');
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var CalendarEventForm = (function (_super) {
            __extends(CalendarEventForm, _super);
            function CalendarEventForm() {
                _super.apply(this, arguments);
            }
            CalendarEventForm.formKey = 'Administration.CalendarEvent';
            return CalendarEventForm;
        }(Serenity.PrefixedContext));
        Administration.CalendarEventForm = CalendarEventForm;
        [['Title', function () { return Serenity.StringEditor; }], ['AllDay', function () { return Serenity.BooleanEditor; }], ['StartDate', function () { return Serenity.DateEditor; }], ['EndDate', function () { return Serenity.DateEditor; }], ['Url', function () { return Serenity.StringEditor; }], ['ClassName', function () { return Serenity.StringEditor; }], ['IsEditable', function () { return Serenity.BooleanEditor; }], ['IsOverlap', function () { return Serenity.BooleanEditor; }], ['Color', function () { return Serenity.StringEditor; }], ['BackgroundColor', function () { return Serenity.StringEditor; }], ['TextColor', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(CalendarEventForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var CalendarEventRow;
        (function (CalendarEventRow) {
            CalendarEventRow.idProperty = 'Id';
            CalendarEventRow.nameProperty = 'Title';
            CalendarEventRow.localTextPrefix = 'Administration.CalendarEvent';
            var Fields;
            (function (Fields) {
            })(Fields = CalendarEventRow.Fields || (CalendarEventRow.Fields = {}));
            ['Id', 'Title', 'AllDay', 'StartDate', 'EndDate', 'Url', 'ClassName', 'IsEditable', 'IsOverlap', 'Color', 'BackgroundColor', 'TextColor'].forEach(function (x) { return Fields[x] = x; });
        })(CalendarEventRow = Administration.CalendarEventRow || (Administration.CalendarEventRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var CalendarEventService;
        (function (CalendarEventService) {
            CalendarEventService.baseUrl = 'Administration/CalendarEvent';
            var Methods;
            (function (Methods) {
            })(Methods = CalendarEventService.Methods || (CalendarEventService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                CalendarEventService[x] = function (r, s, o) { return Q.serviceRequest(CalendarEventService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = CalendarEventService.baseUrl + '/' + x;
            });
        })(CalendarEventService = Administration.CalendarEventService || (Administration.CalendarEventService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LanguageForm = (function (_super) {
            __extends(LanguageForm, _super);
            function LanguageForm() {
                _super.apply(this, arguments);
            }
            LanguageForm.formKey = 'Administration.Language';
            return LanguageForm;
        }(Serenity.PrefixedContext));
        Administration.LanguageForm = LanguageForm;
        [['LanguageId', function () { return Serenity.StringEditor; }], ['LanguageName', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(LanguageForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LanguageRow;
        (function (LanguageRow) {
            LanguageRow.idProperty = 'Id';
            LanguageRow.nameProperty = 'LanguageName';
            LanguageRow.localTextPrefix = 'Administration.Language';
            LanguageRow.lookupKey = 'Administration.Language';
            function getLookup() {
                return Q.getLookup('Administration.Language');
            }
            LanguageRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = LanguageRow.Fields || (LanguageRow.Fields = {}));
            ['Id', 'LanguageId', 'LanguageName'].forEach(function (x) { return Fields[x] = x; });
        })(LanguageRow = Administration.LanguageRow || (Administration.LanguageRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LanguageService;
        (function (LanguageService) {
            LanguageService.baseUrl = 'Administration/Language';
            var Methods;
            (function (Methods) {
            })(Methods = LanguageService.Methods || (LanguageService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                LanguageService[x] = function (r, s, o) { return Q.serviceRequest(LanguageService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = LanguageService.baseUrl + '/' + x;
            });
        })(LanguageService = Administration.LanguageService || (Administration.LanguageService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LogForm = (function (_super) {
            __extends(LogForm, _super);
            function LogForm() {
                _super.apply(this, arguments);
            }
            LogForm.formKey = 'Administration.Log';
            return LogForm;
        }(Serenity.PrefixedContext));
        Administration.LogForm = LogForm;
        [['TableName', function () { return Serenity.StringEditor; }], ['PersianTableName', function () { return Serenity.StringEditor; }], ['RecordId', function () { return Serenity.StringEditor; }], ['RecordName', function () { return Serenity.IntegerEditor; }], ['ActionId', function () { return Serenity.EnumEditor; }], ['UserId', function () { return Serenity.LookupEditor; }], ['InsertDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(LogForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LogRow;
        (function (LogRow) {
            LogRow.idProperty = 'Id';
            LogRow.nameProperty = 'TableName';
            LogRow.localTextPrefix = 'Administration.Log';
            var Fields;
            (function (Fields) {
            })(Fields = LogRow.Fields || (LogRow.Fields = {}));
            ['Id', 'TableName', 'PersianTableName', 'RecordId', 'RecordName', 'IP', 'ActionID', 'UserId', 'InsertDate', 'DisplayName', 'ProvinceId', 'ProvinceName', 'OldData'].forEach(function (x) { return Fields[x] = x; });
        })(LogRow = Administration.LogRow || (Administration.LogRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var LogService;
        (function (LogService) {
            LogService.baseUrl = 'Administration/Log';
            var Methods;
            (function (Methods) {
            })(Methods = LogService.Methods || (LogService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'LogProvinceReport', 'LogLeaderReport'].forEach(function (x) {
                LogService[x] = function (r, s, o) { return Q.serviceRequest(LogService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = LogService.baseUrl + '/' + x;
            });
        })(LogService = Administration.LogService || (Administration.LogService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationForm = (function (_super) {
            __extends(NotificationForm, _super);
            function NotificationForm() {
                _super.apply(this, arguments);
            }
            NotificationForm.formKey = 'Administration.Notification';
            return NotificationForm;
        }(Serenity.PrefixedContext));
        Administration.NotificationForm = NotificationForm;
        [['GroupId', function () { return Serenity.IntegerEditor; }], ['UserId', function () { return Serenity.IntegerEditor; }], ['Message', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(NotificationForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGroupForm = (function (_super) {
            __extends(NotificationGroupForm, _super);
            function NotificationGroupForm() {
                _super.apply(this, arguments);
            }
            NotificationGroupForm.formKey = 'Administration.NotificationGroup';
            return NotificationGroupForm;
        }(Serenity.PrefixedContext));
        Administration.NotificationGroupForm = NotificationGroupForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(NotificationGroupForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGroupRow;
        (function (NotificationGroupRow) {
            NotificationGroupRow.idProperty = 'Id';
            NotificationGroupRow.nameProperty = 'Name';
            NotificationGroupRow.localTextPrefix = 'Administration.NotificationGroup';
            var Fields;
            (function (Fields) {
            })(Fields = NotificationGroupRow.Fields || (NotificationGroupRow.Fields = {}));
            ['Id', 'Name'].forEach(function (x) { return Fields[x] = x; });
        })(NotificationGroupRow = Administration.NotificationGroupRow || (Administration.NotificationGroupRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationGroupService;
        (function (NotificationGroupService) {
            NotificationGroupService.baseUrl = 'Administration/NotificationGroup';
            var Methods;
            (function (Methods) {
            })(Methods = NotificationGroupService.Methods || (NotificationGroupService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                NotificationGroupService[x] = function (r, s, o) { return Q.serviceRequest(NotificationGroupService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = NotificationGroupService.baseUrl + '/' + x;
            });
        })(NotificationGroupService = Administration.NotificationGroupService || (Administration.NotificationGroupService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationRow;
        (function (NotificationRow) {
            NotificationRow.idProperty = 'Id';
            NotificationRow.nameProperty = 'Message';
            NotificationRow.localTextPrefix = 'Administration.Notification';
            var Fields;
            (function (Fields) {
            })(Fields = NotificationRow.Fields || (NotificationRow.Fields = {}));
            ['Id', 'GroupId', 'UserId', 'Message', 'InsertDate'].forEach(function (x) { return Fields[x] = x; });
        })(NotificationRow = Administration.NotificationRow || (Administration.NotificationRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var NotificationService;
        (function (NotificationService) {
            NotificationService.baseUrl = 'Administration/Notification';
            var Methods;
            (function (Methods) {
            })(Methods = NotificationService.Methods || (NotificationService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                NotificationService[x] = function (r, s, o) { return Q.serviceRequest(NotificationService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = NotificationService.baseUrl + '/' + x;
            });
        })(NotificationService = Administration.NotificationService || (Administration.NotificationService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleForm = (function (_super) {
            __extends(RoleForm, _super);
            function RoleForm() {
                _super.apply(this, arguments);
            }
            RoleForm.formKey = 'Administration.Role';
            return RoleForm;
        }(Serenity.PrefixedContext));
        Administration.RoleForm = RoleForm;
        [['RoleName', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(RoleForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RolePermissionRow;
        (function (RolePermissionRow) {
            RolePermissionRow.idProperty = 'RolePermissionId';
            RolePermissionRow.nameProperty = 'PermissionKey';
            RolePermissionRow.localTextPrefix = 'Administration.RolePermission';
            var Fields;
            (function (Fields) {
            })(Fields = RolePermissionRow.Fields || (RolePermissionRow.Fields = {}));
            ['RolePermissionId', 'RoleId', 'PermissionKey', 'RoleRoleName'].forEach(function (x) { return Fields[x] = x; });
        })(RolePermissionRow = Administration.RolePermissionRow || (Administration.RolePermissionRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RolePermissionService;
        (function (RolePermissionService) {
            RolePermissionService.baseUrl = 'Administration/RolePermission';
            var Methods;
            (function (Methods) {
            })(Methods = RolePermissionService.Methods || (RolePermissionService.Methods = {}));
            ['Update', 'List'].forEach(function (x) {
                RolePermissionService[x] = function (r, s, o) { return Q.serviceRequest(RolePermissionService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = RolePermissionService.baseUrl + '/' + x;
            });
        })(RolePermissionService = Administration.RolePermissionService || (Administration.RolePermissionService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleRow;
        (function (RoleRow) {
            RoleRow.idProperty = 'RoleId';
            RoleRow.nameProperty = 'RoleName';
            RoleRow.localTextPrefix = 'Administration.Role';
            RoleRow.lookupKey = 'Administration.Role';
            function getLookup() {
                return Q.getLookup('Administration.Role');
            }
            RoleRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = RoleRow.Fields || (RoleRow.Fields = {}));
            ['RoleId', 'RoleName'].forEach(function (x) { return Fields[x] = x; });
        })(RoleRow = Administration.RoleRow || (Administration.RoleRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleService;
        (function (RoleService) {
            RoleService.baseUrl = 'Administration/Role';
            var Methods;
            (function (Methods) {
            })(Methods = RoleService.Methods || (RoleService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                RoleService[x] = function (r, s, o) { return Q.serviceRequest(RoleService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = RoleService.baseUrl + '/' + x;
            });
        })(RoleService = Administration.RoleService || (Administration.RoleService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleStepRow;
        (function (RoleStepRow) {
            RoleStepRow.idProperty = 'Id';
            RoleStepRow.localTextPrefix = 'Administration.RoleStep';
            var Fields;
            (function (Fields) {
            })(Fields = RoleStepRow.Fields || (RoleStepRow.Fields = {}));
            ['Id', 'RoleId', 'StepId', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'RoleRoleName', 'StepName', 'StepCreatedUserId', 'StepCreatedDate', 'StepModifiedUserId', 'StepModifiedDate', 'StepIsDeleted', 'StepDeletedUserId', 'StepDeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(RoleStepRow = Administration.RoleStepRow || (Administration.RoleStepRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var RoleStepService;
        (function (RoleStepService) {
            RoleStepService.baseUrl = 'Administration/RoleStep';
            var Methods;
            (function (Methods) {
            })(Methods = RoleStepService.Methods || (RoleStepService.Methods = {}));
            ['Update', 'List'].forEach(function (x) {
                RoleStepService[x] = function (r, s, o) { return Q.serviceRequest(RoleStepService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = RoleStepService.baseUrl + '/' + x;
            });
        })(RoleStepService = Administration.RoleStepService || (Administration.RoleStepService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var TranslationService;
        (function (TranslationService) {
            TranslationService.baseUrl = 'Administration/Translation';
            var Methods;
            (function (Methods) {
            })(Methods = TranslationService.Methods || (TranslationService.Methods = {}));
            ['List', 'Update'].forEach(function (x) {
                TranslationService[x] = function (r, s, o) { return Q.serviceRequest(TranslationService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = TranslationService.baseUrl + '/' + x;
            });
        })(TranslationService = Administration.TranslationService || (Administration.TranslationService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserForm = (function (_super) {
            __extends(UserForm, _super);
            function UserForm() {
                _super.apply(this, arguments);
            }
            UserForm.formKey = 'Administration.User';
            return UserForm;
        }(Serenity.PrefixedContext));
        Administration.UserForm = UserForm;
        [['Username', function () { return Serenity.StringEditor; }], ['DisplayName', function () { return Serenity.StringEditor; }], ['EmployeeID', function () { return Serenity.StringEditor; }], ['Email', function () { return Serenity.StringEditor; }], ['Rank', function () { return Serenity.StringEditor; }], ['Password', function () { return Serenity.PasswordEditor; }], ['PasswordConfirm', function () { return Serenity.PasswordEditor; }], ['TelephoneNo1', function () { return Serenity.StringEditor; }], ['MobileNo', function () { return Serenity.StringEditor; }], ['Degree', function () { return Serenity.StringEditor; }], ['ProvinceId', function () { return Serenity.LookupEditor; }], ['ProvinceList', function () { return Serenity.LookupEditor; }], ['IsActive', function () { return Serenity.BooleanEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['ImagePath', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(UserForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserPermissionRow;
        (function (UserPermissionRow) {
            UserPermissionRow.idProperty = 'UserPermissionId';
            UserPermissionRow.nameProperty = 'PermissionKey';
            UserPermissionRow.localTextPrefix = 'Administration.UserPermission';
            var Fields;
            (function (Fields) {
            })(Fields = UserPermissionRow.Fields || (UserPermissionRow.Fields = {}));
            ['UserPermissionId', 'UserId', 'PermissionKey', 'Granted', 'Username', 'User'].forEach(function (x) { return Fields[x] = x; });
        })(UserPermissionRow = Administration.UserPermissionRow || (Administration.UserPermissionRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserPermissionService;
        (function (UserPermissionService) {
            UserPermissionService.baseUrl = 'Administration/UserPermission';
            var Methods;
            (function (Methods) {
            })(Methods = UserPermissionService.Methods || (UserPermissionService.Methods = {}));
            ['Update', 'List', 'ListRolePermissions', 'ListPermissionKeys'].forEach(function (x) {
                UserPermissionService[x] = function (r, s, o) { return Q.serviceRequest(UserPermissionService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = UserPermissionService.baseUrl + '/' + x;
            });
        })(UserPermissionService = Administration.UserPermissionService || (Administration.UserPermissionService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserRoleRow;
        (function (UserRoleRow) {
            UserRoleRow.idProperty = 'UserRoleId';
            UserRoleRow.localTextPrefix = 'Administration.UserRole';
            var Fields;
            (function (Fields) {
            })(Fields = UserRoleRow.Fields || (UserRoleRow.Fields = {}));
            ['UserRoleId', 'UserId', 'RoleId', 'Username', 'User'].forEach(function (x) { return Fields[x] = x; });
        })(UserRoleRow = Administration.UserRoleRow || (Administration.UserRoleRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserRoleService;
        (function (UserRoleService) {
            UserRoleService.baseUrl = 'Administration/UserRole';
            var Methods;
            (function (Methods) {
            })(Methods = UserRoleService.Methods || (UserRoleService.Methods = {}));
            ['Update', 'List'].forEach(function (x) {
                UserRoleService[x] = function (r, s, o) { return Q.serviceRequest(UserRoleService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = UserRoleService.baseUrl + '/' + x;
            });
        })(UserRoleService = Administration.UserRoleService || (Administration.UserRoleService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserRow;
        (function (UserRow) {
            UserRow.idProperty = 'UserId';
            UserRow.isActiveProperty = 'IsActive';
            UserRow.nameProperty = 'DisplayName';
            UserRow.localTextPrefix = 'Administration.User';
            UserRow.lookupKey = 'Administration.User';
            function getLookup() {
                return Q.getLookup('Administration.User');
            }
            UserRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = UserRow.Fields || (UserRow.Fields = {}));
            ['UserId', 'Username', 'Source', 'PasswordHash', 'PasswordSalt', 'DisplayName', 'EmployeeID', 'Rank', 'Email', 'LastDirectoryUpdate', 'IsActive', 'Password', 'PasswordConfirm', 'TelephoneNo1', 'TelephoneNo2', 'MobileNo', 'Degree', 'ProvinceId', 'ProvinceName', 'IsIranTCI', 'ProvinceList', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'LastLoginDate', 'ImagePath', 'InsertUserId', 'InsertDate', 'UpdateUserId', 'UpdateDate'].forEach(function (x) { return Fields[x] = x; });
        })(UserRow = Administration.UserRow || (Administration.UserRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserService;
        (function (UserService) {
            UserService.baseUrl = 'Administration/User';
            var Methods;
            (function (Methods) {
            })(Methods = UserService.Methods || (UserService.Methods = {}));
            ['Create', 'Update', 'Undelete', 'Retrieve', 'List'].forEach(function (x) {
                UserService[x] = function (r, s, o) { return Q.serviceRequest(UserService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = UserService.baseUrl + '/' + x;
            });
        })(UserService = Administration.UserService || (Administration.UserService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserSupportGroupForm = (function (_super) {
            __extends(UserSupportGroupForm, _super);
            function UserSupportGroupForm() {
                _super.apply(this, arguments);
            }
            UserSupportGroupForm.formKey = 'Administration.UserSupportGroup';
            return UserSupportGroupForm;
        }(Serenity.PrefixedContext));
        Administration.UserSupportGroupForm = UserSupportGroupForm;
        [['UserId', function () { return Serenity.IntegerEditor; }], ['GroupId', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(UserSupportGroupForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserSupportGroupRow;
        (function (UserSupportGroupRow) {
            UserSupportGroupRow.idProperty = 'Id';
            UserSupportGroupRow.localTextPrefix = 'Administration.UserSupportGroup';
            var Fields;
            (function (Fields) {
            })(Fields = UserSupportGroupRow.Fields || (UserSupportGroupRow.Fields = {}));
            ['Id', 'UserId', 'GroupId', 'UserOldcaseId', 'UserUsername', 'UserDisplayName', 'UserEmployeeId', 'UserEmail', 'UserRank', 'UserSource', 'UserPassword', 'UserPasswordHash', 'UserPasswordSalt', 'UserInsertDate', 'UserInsertUserId', 'UserUpdateDate', 'UserUpdateUserId', 'UserIsActive', 'UserLastDirectoryUpdate', 'UserRoleId', 'UserTelephoneNo1', 'UserTelephoneNo2', 'UserMobileNo', 'UserDegree', 'UserProvinceId'].forEach(function (x) { return Fields[x] = x; });
        })(UserSupportGroupRow = Administration.UserSupportGroupRow || (Administration.UserSupportGroupRow = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        var UserSupportGroupService;
        (function (UserSupportGroupService) {
            UserSupportGroupService.baseUrl = 'Administration/UserSupportGroup';
            var Methods;
            (function (Methods) {
            })(Methods = UserSupportGroupService.Methods || (UserSupportGroupService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                UserSupportGroupService[x] = function (r, s, o) { return Q.serviceRequest(UserSupportGroupService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = UserSupportGroupService.baseUrl + '/' + x;
            });
        })(UserSupportGroupService = Administration.UserSupportGroupService || (Administration.UserSupportGroupService = {}));
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Administration;
    (function (Administration) {
        (function (UserTCI) {
            UserTCI[UserTCI["Iran"] = 1] = "Iran";
        })(Administration.UserTCI || (Administration.UserTCI = {}));
        var UserTCI = Administration.UserTCI;
        Serenity.Decorators.registerEnum(UserTCI, 'Administration.UserTCI');
    })(Administration = CaseManagement.Administration || (CaseManagement.Administration = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationForm = (function (_super) {
            __extends(ActivityCorrectionOperationForm, _super);
            function ActivityCorrectionOperationForm() {
                _super.apply(this, arguments);
            }
            ActivityCorrectionOperationForm.formKey = 'Case.ActivityCorrectionOperation';
            return ActivityCorrectionOperationForm;
        }(Serenity.PrefixedContext));
        Case.ActivityCorrectionOperationForm = ActivityCorrectionOperationForm;
        [['Body', function () { return Serenity.TextAreaEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityCorrectionOperationForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationRow;
        (function (ActivityCorrectionOperationRow) {
            ActivityCorrectionOperationRow.idProperty = 'Id';
            ActivityCorrectionOperationRow.nameProperty = 'Body';
            ActivityCorrectionOperationRow.localTextPrefix = 'Case.ActivityCorrectionOperation';
            ActivityCorrectionOperationRow.lookupKey = 'Case.ActivityCorrectionOperation';
            function getLookup() {
                return Q.getLookup('Case.ActivityCorrectionOperation');
            }
            ActivityCorrectionOperationRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityCorrectionOperationRow.Fields || (ActivityCorrectionOperationRow.Fields = {}));
            ['Id', 'ActivityId', 'Body'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityCorrectionOperationRow = Case.ActivityCorrectionOperationRow || (Case.ActivityCorrectionOperationRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityCorrectionOperationService;
        (function (ActivityCorrectionOperationService) {
            ActivityCorrectionOperationService.baseUrl = 'Case/ActivityCorrectionOperation';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityCorrectionOperationService.Methods || (ActivityCorrectionOperationService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityCorrectionOperationService[x] = function (r, s, o) { return Q.serviceRequest(ActivityCorrectionOperationService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityCorrectionOperationService.baseUrl + '/' + x;
            });
        })(ActivityCorrectionOperationService = Case.ActivityCorrectionOperationService || (Case.ActivityCorrectionOperationService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityForm = (function (_super) {
            __extends(ActivityForm, _super);
            function ActivityForm() {
                _super.apply(this, arguments);
            }
            ActivityForm.formKey = 'Case.Activity';
            return ActivityForm;
        }(Serenity.PrefixedContext));
        Case.ActivityForm = ActivityForm;
        [['Code', function () { return Serenity.IntegerEditor; }], ['Name', function () { return Serenity.TextAreaEditor; }], ['EnglishName', function () { return Serenity.TextAreaEditor; }], ['Objective', function () { return Serenity.TextAreaEditor; }], ['EnglishObjective', function () { return Serenity.TextAreaEditor; }], ['GroupId', function () { return Serenity.LookupEditor; }], ['RepeatTermId', function () { return Serenity.LookupEditor; }], ['RequiredYearRepeatCount', function () { return Serenity.LookupEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['KeyCheckArea', function () { return Serenity.TextAreaEditor; }], ['DataSource', function () { return Serenity.TextAreaEditor; }], ['Methodology', function () { return Serenity.TextAreaEditor; }], ['KeyFocus', function () { return Serenity.TextAreaEditor; }], ['Action', function () { return Serenity.TextAreaEditor; }], ['KPI', function () { return Serenity.TextAreaEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReasonList', function () { return Case.ActivityMainReasonEditor; }], ['CorrectionOperationList', function () { return Case.ActivityCorrectionOperationEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGroupForm = (function (_super) {
            __extends(ActivityGroupForm, _super);
            function ActivityGroupForm() {
                _super.apply(this, arguments);
            }
            ActivityGroupForm.formKey = 'Case.ActivityGroup';
            return ActivityGroupForm;
        }(Serenity.PrefixedContext));
        Case.ActivityGroupForm = ActivityGroupForm;
        [['Name', function () { return Serenity.StringEditor; }], ['EnglishName', function () { return Serenity.StringEditor; }], ['Code', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityGroupForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGroupRow;
        (function (ActivityGroupRow) {
            ActivityGroupRow.idProperty = 'Id';
            ActivityGroupRow.nameProperty = 'Name';
            ActivityGroupRow.localTextPrefix = 'Case.ActivityGroup';
            ActivityGroupRow.lookupKey = 'Case.ActivityGroup';
            function getLookup() {
                return Q.getLookup('Case.ActivityGroup');
            }
            ActivityGroupRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityGroupRow.Fields || (ActivityGroupRow.Fields = {}));
            ['Id', 'Name', 'EnglishName', 'Code'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityGroupRow = Case.ActivityGroupRow || (Case.ActivityGroupRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityGroupService;
        (function (ActivityGroupService) {
            ActivityGroupService.baseUrl = 'Case/ActivityGroup';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityGroupService.Methods || (ActivityGroupService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityGroupService[x] = function (r, s, o) { return Q.serviceRequest(ActivityGroupService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityGroupService.baseUrl + '/' + x;
            });
        })(ActivityGroupService = Case.ActivityGroupService || (Case.ActivityGroupService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityHelpForm = (function (_super) {
            __extends(ActivityHelpForm, _super);
            function ActivityHelpForm() {
                _super.apply(this, arguments);
            }
            ActivityHelpForm.formKey = 'Case.ActivityHelp';
            return ActivityHelpForm;
        }(Serenity.PrefixedContext));
        Case.ActivityHelpForm = ActivityHelpForm;
        [['Code', function () { return Serenity.IntegerEditor; }], ['Name', function () { return Serenity.TextAreaEditor; }], ['EnglishName', function () { return Serenity.TextAreaEditor; }], ['Objective', function () { return Serenity.TextAreaEditor; }], ['EnglishObjective', function () { return Serenity.TextAreaEditor; }], ['GroupId', function () { return Serenity.LookupEditor; }], ['RepeatTermId', function () { return Serenity.LookupEditor; }], ['KeyCheckArea', function () { return Serenity.TextAreaEditor; }], ['DataSource', function () { return Serenity.TextAreaEditor; }], ['Methodology', function () { return Serenity.TextAreaEditor; }], ['KeyFocus', function () { return Serenity.TextAreaEditor; }], ['Action', function () { return Serenity.TextAreaEditor; }], ['KPI', function () { return Serenity.TextAreaEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityHelpForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityHelpRow;
        (function (ActivityHelpRow) {
            ActivityHelpRow.idProperty = 'Id';
            ActivityHelpRow.nameProperty = 'CodeName';
            ActivityHelpRow.localTextPrefix = 'Case.ActivityHelp';
            ActivityHelpRow.lookupKey = 'Case.ActivityHelp';
            function getLookup() {
                return Q.getLookup('Case.ActivityHelp');
            }
            ActivityHelpRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityHelpRow.Fields || (ActivityHelpRow.Fields = {}));
            ['Id', 'Code', 'Name', 'CodeName', 'EnglishName', 'Objective', 'EnglishObjective', 'GroupId', 'RepeatTermId', 'GroupName', 'RepeatTermName', 'KeyCheckArea', 'DataSource', 'Methodology', 'KeyFocus', 'Action', 'KPI', 'EventDescription'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityHelpRow = Case.ActivityHelpRow || (Case.ActivityHelpRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityHelpService;
        (function (ActivityHelpService) {
            ActivityHelpService.baseUrl = 'Case/ActivityHelp';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityHelpService.Methods || (ActivityHelpService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityHelpService[x] = function (r, s, o) { return Q.serviceRequest(ActivityHelpService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityHelpService.baseUrl + '/' + x;
            });
        })(ActivityHelpService = Case.ActivityHelpService || (Case.ActivityHelpService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonForm = (function (_super) {
            __extends(ActivityMainReasonForm, _super);
            function ActivityMainReasonForm() {
                _super.apply(this, arguments);
            }
            ActivityMainReasonForm.formKey = 'Case.ActivityMainReason';
            return ActivityMainReasonForm;
        }(Serenity.PrefixedContext));
        Case.ActivityMainReasonForm = ActivityMainReasonForm;
        [['Body', function () { return Serenity.TextAreaEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityMainReasonForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonRow;
        (function (ActivityMainReasonRow) {
            ActivityMainReasonRow.idProperty = 'Id';
            ActivityMainReasonRow.nameProperty = 'Body';
            ActivityMainReasonRow.localTextPrefix = 'Case.ActivityMainReason';
            ActivityMainReasonRow.lookupKey = 'Case.ActivityMainReason';
            function getLookup() {
                return Q.getLookup('Case.ActivityMainReason');
            }
            ActivityMainReasonRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityMainReasonRow.Fields || (ActivityMainReasonRow.Fields = {}));
            ['Id', 'ActivityId', 'Body', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityMainReasonRow = Case.ActivityMainReasonRow || (Case.ActivityMainReasonRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityMainReasonService;
        (function (ActivityMainReasonService) {
            ActivityMainReasonService.baseUrl = 'Case/ActivityMainReason';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityMainReasonService.Methods || (ActivityMainReasonService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityMainReasonService[x] = function (r, s, o) { return Q.serviceRequest(ActivityMainReasonService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityMainReasonService.baseUrl + '/' + x;
            });
        })(ActivityMainReasonService = Case.ActivityMainReasonService || (Case.ActivityMainReasonService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentForm = (function (_super) {
            __extends(ActivityRequestCommentForm, _super);
            function ActivityRequestCommentForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestCommentForm.formKey = 'Case.ActivityRequestComment';
            return ActivityRequestCommentForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestCommentForm = ActivityRequestCommentForm;
        [['Comment', function () { return Serenity.TextAreaEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestCommentForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentReasonForm = (function (_super) {
            __extends(ActivityRequestCommentReasonForm, _super);
            function ActivityRequestCommentReasonForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestCommentReasonForm.formKey = 'Case.ActivityRequestCommentReason';
            return ActivityRequestCommentReasonForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestCommentReasonForm = ActivityRequestCommentReasonForm;
        [['CommentReasonId', function () { return Serenity.IntegerEditor; }], ['ActivityRequestId', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestCommentReasonForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentReasonRow;
        (function (ActivityRequestCommentReasonRow) {
            ActivityRequestCommentReasonRow.idProperty = 'Id';
            ActivityRequestCommentReasonRow.localTextPrefix = 'Case.ActivityRequestCommentReason';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestCommentReasonRow.Fields || (ActivityRequestCommentReasonRow.Fields = {}));
            ['Id', 'CommentReasonId', 'ActivityRequestId', 'CommentReasonComment', 'CommentReasonCreatedUserId', 'CommentReasonCreatedDate', 'ActivityRequestOldCaseId', 'ActivityRequestProvinceId', 'ActivityRequestActivityId', 'ActivityRequestCycleId', 'ActivityRequestCustomerEffectId', 'ActivityRequestRiskLevelId', 'ActivityRequestIncomeFlowId', 'ActivityRequestCount', 'ActivityRequestCycleCost', 'ActivityRequestFactor', 'ActivityRequestDelayedCost', 'ActivityRequestYearCost', 'ActivityRequestAccessibleCost', 'ActivityRequestInaccessibleCost', 'ActivityRequestFinancial', 'ActivityRequestLeakDate', 'ActivityRequestDiscoverLeakDate', 'ActivityRequestDiscoverLeakDateShamsi', 'ActivityRequestEventDescription', 'ActivityRequestMainReason', 'ActivityRequestCorrectionOperation', 'ActivityRequestAvoidRepeatingOperation', 'ActivityRequestCreatedUserId', 'ActivityRequestCreatedDate', 'ActivityRequestCreatedDateShamsi', 'ActivityRequestModifiedUserId', 'ActivityRequestModifiedDate', 'ActivityRequestIsDeleted', 'ActivityRequestDeletedUserId', 'ActivityRequestDeletedDate', 'ActivityRequestEndDate', 'ActivityRequestStatusId', 'ActivityRequestActionId'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestCommentReasonRow = Case.ActivityRequestCommentReasonRow || (Case.ActivityRequestCommentReasonRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentReasonService;
        (function (ActivityRequestCommentReasonService) {
            ActivityRequestCommentReasonService.baseUrl = 'Case/ActivityRequestCommentReason';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestCommentReasonService.Methods || (ActivityRequestCommentReasonService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestCommentReasonService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestCommentReasonService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestCommentReasonService.baseUrl + '/' + x;
            });
        })(ActivityRequestCommentReasonService = Case.ActivityRequestCommentReasonService || (Case.ActivityRequestCommentReasonService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentRow;
        (function (ActivityRequestCommentRow) {
            ActivityRequestCommentRow.idProperty = 'Id';
            ActivityRequestCommentRow.nameProperty = 'Comment';
            ActivityRequestCommentRow.localTextPrefix = 'Case.ActivityRequestComment';
            ActivityRequestCommentRow.lookupKey = 'Case.ActivityRequestComment';
            function getLookup() {
                return Q.getLookup('Case.ActivityRequestComment');
            }
            ActivityRequestCommentRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestCommentRow.Fields || (ActivityRequestCommentRow.Fields = {}));
            ['Id', 'Comment', 'ActivityRequestId', 'CreatedUserId', 'CreatedDate', 'CreatedUserName'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestCommentRow = Case.ActivityRequestCommentRow || (Case.ActivityRequestCommentRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestCommentService;
        (function (ActivityRequestCommentService) {
            ActivityRequestCommentService.baseUrl = 'Case/ActivityRequestComment';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestCommentService.Methods || (ActivityRequestCommentService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestCommentService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestCommentService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestCommentService.baseUrl + '/' + x;
            });
        })(ActivityRequestCommentService = Case.ActivityRequestCommentService || (Case.ActivityRequestCommentService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmAdminForm = (function (_super) {
            __extends(ActivityRequestConfirmAdminForm, _super);
            function ActivityRequestConfirmAdminForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestConfirmAdminForm.formKey = 'Case.ActivityRequestConfirmAdmin';
            return ActivityRequestConfirmAdminForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestConfirmAdminForm = ActivityRequestConfirmAdminForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.DecimalEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ActionID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestConfirmAdminForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmAdminRow;
        (function (ActivityRequestConfirmAdminRow) {
            ActivityRequestConfirmAdminRow.idProperty = 'Id';
            ActivityRequestConfirmAdminRow.localTextPrefix = 'Case.ActivityRequestConfirmAdmin';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestConfirmAdminRow.Fields || (ActivityRequestConfirmAdminRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestConfirmAdminRow = Case.ActivityRequestConfirmAdminRow || (Case.ActivityRequestConfirmAdminRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmAdminService;
        (function (ActivityRequestConfirmAdminService) {
            ActivityRequestConfirmAdminService.baseUrl = 'Case/ActivityRequestConfirmAdmin';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestConfirmAdminService.Methods || (ActivityRequestConfirmAdminService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestConfirmAdminService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestConfirmAdminService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestConfirmAdminService.baseUrl + '/' + x;
            });
        })(ActivityRequestConfirmAdminService = Case.ActivityRequestConfirmAdminService || (Case.ActivityRequestConfirmAdminService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmForm = (function (_super) {
            __extends(ActivityRequestConfirmForm, _super);
            function ActivityRequestConfirmForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestConfirmForm.formKey = 'Case.ActivityRequestConfirm';
            return ActivityRequestConfirmForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestConfirmForm = ActivityRequestConfirmForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['CycleCostHistory', function () { return Serenity.DecimalEditor; }], ['DelayedCostHistory', function () { return Serenity.DecimalEditor; }], ['AccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['InaccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.DecimalEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestConfirmForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmRow;
        (function (ActivityRequestConfirmRow) {
            ActivityRequestConfirmRow.idProperty = 'Id';
            ActivityRequestConfirmRow.localTextPrefix = 'Case.ActivityRequestConfirm';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestConfirmRow.Fields || (ActivityRequestConfirmRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestConfirmRow = Case.ActivityRequestConfirmRow || (Case.ActivityRequestConfirmRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestConfirmService;
        (function (ActivityRequestConfirmService) {
            ActivityRequestConfirmService.baseUrl = 'Case/ActivityRequestConfirm';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestConfirmService.Methods || (ActivityRequestConfirmService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestConfirmService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestConfirmService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestConfirmService.baseUrl + '/' + x;
            });
        })(ActivityRequestConfirmService = Case.ActivityRequestConfirmService || (Case.ActivityRequestConfirmService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDeleteForm = (function (_super) {
            __extends(ActivityRequestDeleteForm, _super);
            function ActivityRequestDeleteForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestDeleteForm.formKey = 'Case.ActivityRequestDelete';
            return ActivityRequestDeleteForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestDeleteForm = ActivityRequestDeleteForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestDeleteForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDeleteRow;
        (function (ActivityRequestDeleteRow) {
            ActivityRequestDeleteRow.idProperty = 'Id';
            ActivityRequestDeleteRow.nameProperty = 'ActivityCode';
            ActivityRequestDeleteRow.localTextPrefix = 'Case.ActivityRequestDelete';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestDeleteRow.Fields || (ActivityRequestDeleteRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestDeleteRow = Case.ActivityRequestDeleteRow || (Case.ActivityRequestDeleteRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDeleteService;
        (function (ActivityRequestDeleteService) {
            ActivityRequestDeleteService.baseUrl = 'Case/ActivityRequestDelete';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestDeleteService.Methods || (ActivityRequestDeleteService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestDeleteService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestDeleteService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestDeleteService.baseUrl + '/' + x;
            });
        })(ActivityRequestDeleteService = Case.ActivityRequestDeleteService || (Case.ActivityRequestDeleteService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDenyForm = (function (_super) {
            __extends(ActivityRequestDenyForm, _super);
            function ActivityRequestDenyForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestDenyForm.formKey = 'Case.ActivityRequestDeny';
            return ActivityRequestDenyForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestDenyForm = ActivityRequestDenyForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['CycleCostHistory', function () { return Serenity.DecimalEditor; }], ['DelayedCostHistory', function () { return Serenity.DecimalEditor; }], ['AccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['InaccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.DecimalEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestDenyForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDenyRow;
        (function (ActivityRequestDenyRow) {
            ActivityRequestDenyRow.idProperty = 'Id';
            ActivityRequestDenyRow.localTextPrefix = 'Case.ActivityRequestDeny';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestDenyRow.Fields || (ActivityRequestDenyRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestDenyRow = Case.ActivityRequestDenyRow || (Case.ActivityRequestDenyRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDenyService;
        (function (ActivityRequestDenyService) {
            ActivityRequestDenyService.baseUrl = 'Case/ActivityRequestDeny';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestDenyService.Methods || (ActivityRequestDenyService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestDenyService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestDenyService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestDenyService.baseUrl + '/' + x;
            });
        })(ActivityRequestDenyService = Case.ActivityRequestDenyService || (Case.ActivityRequestDenyService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDetailsInfoForm = (function (_super) {
            __extends(ActivityRequestDetailsInfoForm, _super);
            function ActivityRequestDetailsInfoForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestDetailsInfoForm.formKey = 'Case.ActivityRequestDetailsInfo';
            return ActivityRequestDetailsInfoForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestDetailsInfoForm = ActivityRequestDetailsInfoForm;
        [['Id', function () { return Serenity.StringEditor; }], ['ProvinceId', function () { return Serenity.IntegerEditor; }], ['ActivityId', function () { return Serenity.IntegerEditor; }], ['CycleId', function () { return Serenity.IntegerEditor; }], ['IncomeFlowId', function () { return Serenity.IntegerEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.StringEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.StringEditor; }], ['YearCost', function () { return Serenity.StringEditor; }], ['AccessibleCost', function () { return Serenity.StringEditor; }], ['InaccessibleCost', function () { return Serenity.StringEditor; }], ['TotalLeakage', function () { return Serenity.StringEditor; }], ['RecoverableLeakage', function () { return Serenity.StringEditor; }], ['Recovered', function () { return Serenity.StringEditor; }], ['DelayedCostHistory', function () { return Serenity.StringEditor; }], ['YearCostHistory', function () { return Serenity.StringEditor; }], ['AccessibleCostHistory', function () { return Serenity.StringEditor; }], ['InaccessibleCostHistory', function () { return Serenity.StringEditor; }], ['RejectCount', function () { return Serenity.IntegerEditor; }], ['EventDescription', function () { return Serenity.StringEditor; }], ['MainReason', function () { return Serenity.StringEditor; }], ['CycleName', function () { return Serenity.StringEditor; }], ['Name', function () { return Serenity.StringEditor; }], ['Expr1', function () { return Serenity.StringEditor; }], ['CodeName', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestDetailsInfoForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDetailsInfoRow;
        (function (ActivityRequestDetailsInfoRow) {
            ActivityRequestDetailsInfoRow.idProperty = 'Id';
            ActivityRequestDetailsInfoRow.nameProperty = 'EventDescription';
            ActivityRequestDetailsInfoRow.localTextPrefix = 'Case.ActivityRequestDetailsInfo';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestDetailsInfoRow.Fields || (ActivityRequestDetailsInfoRow.Fields = {}));
            ['Id', 'ProvinceId', 'ActivityId', 'CycleId', 'IncomeFlowId', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'RejectCount', 'EventDescription', 'MainReason', 'CycleName', 'Name', 'Expr1', 'CodeName', 'DiscoverLeakDate'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestDetailsInfoRow = Case.ActivityRequestDetailsInfoRow || (Case.ActivityRequestDetailsInfoRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestDetailsInfoService;
        (function (ActivityRequestDetailsInfoService) {
            ActivityRequestDetailsInfoService.baseUrl = 'Case/ActivityRequestDetailsInfo';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestDetailsInfoService.Methods || (ActivityRequestDetailsInfoService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestDetailsInfoService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestDetailsInfoService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestDetailsInfoService.baseUrl + '/' + x;
            });
        })(ActivityRequestDetailsInfoService = Case.ActivityRequestDetailsInfoService || (Case.ActivityRequestDetailsInfoService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestFinancialForm = (function (_super) {
            __extends(ActivityRequestFinancialForm, _super);
            function ActivityRequestFinancialForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestFinancialForm.formKey = 'Case.ActivityRequestFinancial';
            return ActivityRequestFinancialForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestFinancialForm = ActivityRequestFinancialForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['CycleCostHistory', function () { return Serenity.DecimalEditor; }], ['DelayedCostHistory', function () { return Serenity.DecimalEditor; }], ['AccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['InaccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['RejectCount', function () { return Serenity.StringEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ConfirmTypeID', function () { return Serenity.EnumEditor; }], ['ActionID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestFinancialForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestFinancialRow;
        (function (ActivityRequestFinancialRow) {
            ActivityRequestFinancialRow.idProperty = 'Id';
            ActivityRequestFinancialRow.localTextPrefix = 'Case.ActivityRequestFinancial';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestFinancialRow.Fields || (ActivityRequestFinancialRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestFinancialRow = Case.ActivityRequestFinancialRow || (Case.ActivityRequestFinancialRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestFinancialService;
        (function (ActivityRequestFinancialService) {
            ActivityRequestFinancialService.baseUrl = 'Case/ActivityRequestFinancial';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestFinancialService.Methods || (ActivityRequestFinancialService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestFinancialService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestFinancialService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestFinancialService.baseUrl + '/' + x;
            });
        })(ActivityRequestFinancialService = Case.ActivityRequestFinancialService || (Case.ActivityRequestFinancialService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestForm = (function (_super) {
            __extends(ActivityRequestForm, _super);
            function ActivityRequestForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestForm.formKey = 'Case.ActivityRequest';
            return ActivityRequestForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestForm = ActivityRequestForm;
        [['ActivityId', function () { return Serenity.LookupEditor; }], ['ProvinceName', function () { return Serenity.StringEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ConfirmTypeID', function () { return Serenity.EnumEditor; }], ['ActionID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestHistoryForm = (function (_super) {
            __extends(ActivityRequestHistoryForm, _super);
            function ActivityRequestHistoryForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestHistoryForm.formKey = 'Case.ActivityRequestHistory';
            return ActivityRequestHistoryForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestHistoryForm = ActivityRequestHistoryForm;
        [['CycleCostHistory', function () { return Serenity.DecimalEditor; }], ['DelayedCostHistory', function () { return Serenity.DecimalEditor; }], ['YearCostHistory', function () { return Serenity.DecimalEditor; }], ['AccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['InaccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['TotalLeakageHistory', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakageHistory', function () { return Serenity.DecimalEditor; }], ['RecoveredHistory', function () { return Serenity.DecimalEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestHistoryForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestHistoryRow;
        (function (ActivityRequestHistoryRow) {
            ActivityRequestHistoryRow.idProperty = 'Id';
            ActivityRequestHistoryRow.localTextPrefix = 'Case.ActivityRequestHistory';
            ActivityRequestHistoryRow.lookupKey = 'Case.ActivityRequestHistory';
            function getLookup() {
                return Q.getLookup('Case.ActivityRequestHistory');
            }
            ActivityRequestHistoryRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestHistoryRow.Fields || (ActivityRequestHistoryRow.Fields = {}));
            ['Id', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestHistoryRow = Case.ActivityRequestHistoryRow || (Case.ActivityRequestHistoryRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestHistoryService;
        (function (ActivityRequestHistoryService) {
            ActivityRequestHistoryService.baseUrl = 'Case/ActivityRequestHistory';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestHistoryService.Methods || (ActivityRequestHistoryService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestHistoryService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestHistoryService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestHistoryService.baseUrl + '/' + x;
            });
        })(ActivityRequestHistoryService = Case.ActivityRequestHistoryService || (Case.ActivityRequestHistoryService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLeaderForm = (function (_super) {
            __extends(ActivityRequestLeaderForm, _super);
            function ActivityRequestLeaderForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestLeaderForm.formKey = 'Case.ActivityRequestLeader';
            return ActivityRequestLeaderForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestLeaderForm = ActivityRequestLeaderForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ConfirmTypeID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestLeaderForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLeaderRow;
        (function (ActivityRequestLeaderRow) {
            ActivityRequestLeaderRow.idProperty = 'Id';
            ActivityRequestLeaderRow.localTextPrefix = 'Case.ActivityRequestLeader';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestLeaderRow.Fields || (ActivityRequestLeaderRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestLeaderRow = Case.ActivityRequestLeaderRow || (Case.ActivityRequestLeaderRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLeaderService;
        (function (ActivityRequestLeaderService) {
            ActivityRequestLeaderService.baseUrl = 'Case/ActivityRequestLeader';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestLeaderService.Methods || (ActivityRequestLeaderService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestLeaderService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestLeaderService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestLeaderService.baseUrl + '/' + x;
            });
        })(ActivityRequestLeaderService = Case.ActivityRequestLeaderService || (Case.ActivityRequestLeaderService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLogForm = (function (_super) {
            __extends(ActivityRequestLogForm, _super);
            function ActivityRequestLogForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestLogForm.formKey = 'Case.ActivityRequestLog';
            return ActivityRequestLogForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestLogForm = ActivityRequestLogForm;
        [['StatusId', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestLogForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLogRow;
        (function (ActivityRequestLogRow) {
            ActivityRequestLogRow.idProperty = 'Id';
            ActivityRequestLogRow.localTextPrefix = 'Case.ActivityRequestLog';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestLogRow.Fields || (ActivityRequestLogRow.Fields = {}));
            ['Id', 'ActivityRequestId', 'StatusId', 'ActionID', 'UserId', 'InsertDate', 'StatusName', 'UserDisplayName'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestLogRow = Case.ActivityRequestLogRow || (Case.ActivityRequestLogRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestLogService;
        (function (ActivityRequestLogService) {
            ActivityRequestLogService.baseUrl = 'Case/ActivityRequestLog';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestLogService.Methods || (ActivityRequestLogService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestLogService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestLogService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestLogService.baseUrl + '/' + x;
            });
        })(ActivityRequestLogService = Case.ActivityRequestLogService || (Case.ActivityRequestLogService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestPenddingForm = (function (_super) {
            __extends(ActivityRequestPenddingForm, _super);
            function ActivityRequestPenddingForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestPenddingForm.formKey = 'Case.ActivityRequestPendding';
            return ActivityRequestPenddingForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestPenddingForm = ActivityRequestPenddingForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['CycleCostHistory', function () { return Serenity.DecimalEditor; }], ['DelayedCostHistory', function () { return Serenity.DecimalEditor; }], ['AccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['InaccessibleCostHistory', function () { return Serenity.DecimalEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.DecimalEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ConfirmTypeID', function () { return Serenity.EnumEditor; }], ['ActionID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestPenddingForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestPenddingRow;
        (function (ActivityRequestPenddingRow) {
            ActivityRequestPenddingRow.idProperty = 'Id';
            ActivityRequestPenddingRow.localTextPrefix = 'Case.ActivityRequestPendding';
            ActivityRequestPenddingRow.lookupKey = 'Case.ActivityRequestPenddingRow';
            function getLookup() {
                return Q.getLookup('Case.ActivityRequestPenddingRow');
            }
            ActivityRequestPenddingRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestPenddingRow.Fields || (ActivityRequestPenddingRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestPenddingRow = Case.ActivityRequestPenddingRow || (Case.ActivityRequestPenddingRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestPenddingService;
        (function (ActivityRequestPenddingService) {
            ActivityRequestPenddingService.baseUrl = 'Case/ActivityRequestPendding';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestPenddingService.Methods || (ActivityRequestPenddingService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestPenddingService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestPenddingService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestPenddingService.baseUrl + '/' + x;
            });
        })(ActivityRequestPenddingService = Case.ActivityRequestPenddingService || (Case.ActivityRequestPenddingService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestRow;
        (function (ActivityRequestRow) {
            ActivityRequestRow.idProperty = 'Id';
            ActivityRequestRow.localTextPrefix = 'Case.ActivityRequest';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestRow.Fields || (ActivityRequestRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestRow = Case.ActivityRequestRow || (Case.ActivityRequestRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestService;
        (function (ActivityRequestService) {
            ActivityRequestService.baseUrl = 'Case/ActivityRequest';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestService.Methods || (ActivityRequestService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestService.baseUrl + '/' + x;
            });
        })(ActivityRequestService = Case.ActivityRequestService || (Case.ActivityRequestService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestTechnicalForm = (function (_super) {
            __extends(ActivityRequestTechnicalForm, _super);
            function ActivityRequestTechnicalForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestTechnicalForm.formKey = 'Case.ActivityRequestTechnical';
            return ActivityRequestTechnicalForm;
        }(Serenity.PrefixedContext));
        Case.ActivityRequestTechnicalForm = ActivityRequestTechnicalForm;
        [['ProvinceName', function () { return Serenity.StringEditor; }], ['Id', function () { return Serenity.StringEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.DecimalEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.DecimalEditor; }], ['AccessibleCost', function () { return Serenity.DecimalEditor; }], ['InaccessibleCost', function () { return Serenity.DecimalEditor; }], ['YearCost', function () { return Serenity.DecimalEditor; }], ['TotalLeakage', function () { return Serenity.DecimalEditor; }], ['RecoverableLeakage', function () { return Serenity.DecimalEditor; }], ['Recovered', function () { return Serenity.DecimalEditor; }], ['RejectCount', function () { return Serenity.StringEditor; }], ['EventDescription', function () { return Serenity.TextAreaEditor; }], ['MainReason', function () { return Serenity.TextAreaEditor; }], ['CommnetList', function () { return Case.ActivityRequestCommentEditor; }], ['File1', function () { return Serenity.ImageUploadEditor; }], ['File2', function () { return Serenity.ImageUploadEditor; }], ['ConfirmTypeID', function () { return Serenity.EnumEditor; }], ['ActionID', function () { return Serenity.EnumEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestTechnicalForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestTechnicalRow;
        (function (ActivityRequestTechnicalRow) {
            ActivityRequestTechnicalRow.idProperty = 'Id';
            ActivityRequestTechnicalRow.localTextPrefix = 'Case.ActivityRequestTechnical';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestTechnicalRow.Fields || (ActivityRequestTechnicalRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ActionID', 'ConfirmTypeID', 'IsRejected', 'RejectCount', 'CommentReasonList', 'CommnetList', 'File1', 'File2', 'File3', 'FinancialControllerConfirm', 'CycleCostHistory', 'DelayedCostHistory', 'YearCostHistory', 'AccessibleCostHistory', 'InaccessibleCostHistory', 'TotalLeakageHistory', 'RecoverableLeakageHistory', 'RecoveredHistory'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestTechnicalRow = Case.ActivityRequestTechnicalRow || (Case.ActivityRequestTechnicalRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRequestTechnicalService;
        (function (ActivityRequestTechnicalService) {
            ActivityRequestTechnicalService.baseUrl = 'Case/ActivityRequestTechnical';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestTechnicalService.Methods || (ActivityRequestTechnicalService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestTechnicalService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestTechnicalService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestTechnicalService.baseUrl + '/' + x;
            });
        })(ActivityRequestTechnicalService = Case.ActivityRequestTechnicalService || (Case.ActivityRequestTechnicalService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityRow;
        (function (ActivityRow) {
            ActivityRow.idProperty = 'Id';
            ActivityRow.nameProperty = 'CodeName';
            ActivityRow.localTextPrefix = 'Case.Activity';
            ActivityRow.lookupKey = 'Case.Activity';
            function getLookup() {
                return Q.getLookup('Case.Activity');
            }
            ActivityRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRow.Fields || (ActivityRow.Fields = {}));
            ['Id', 'Code', 'Name', 'CodeName', 'EnglishName', 'Objective', 'EnglishObjective', 'GroupId', 'RepeatTermId', 'RequiredYearRepeatCount', 'GroupName', 'RepeatTermName', 'KeyCheckArea', 'DataSource', 'Methodology', 'KeyFocus', 'Action', 'KPI', 'EventDescription', 'MainReasonList', 'CorrectionOperationList', 'Factor'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRow = Case.ActivityRow || (Case.ActivityRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ActivityService;
        (function (ActivityService) {
            ActivityService.baseUrl = 'Case/Activity';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityService.Methods || (ActivityService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'ActivitybyGroupList'].forEach(function (x) {
                ActivityService[x] = function (r, s, o) { return Q.serviceRequest(ActivityService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityService.baseUrl + '/' + x;
            });
        })(ActivityService = Case.ActivityService || (Case.ActivityService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CommentReasonForm = (function (_super) {
            __extends(CommentReasonForm, _super);
            function CommentReasonForm() {
                _super.apply(this, arguments);
            }
            CommentReasonForm.formKey = 'Case.CommentReason';
            return CommentReasonForm;
        }(Serenity.PrefixedContext));
        Case.CommentReasonForm = CommentReasonForm;
        [['Comment', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(CommentReasonForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CommentReasonRow;
        (function (CommentReasonRow) {
            CommentReasonRow.idProperty = 'Id';
            CommentReasonRow.nameProperty = 'Comment';
            CommentReasonRow.localTextPrefix = 'Case.CommentReason';
            CommentReasonRow.lookupKey = 'Case.CommentReason';
            function getLookup() {
                return Q.getLookup('Case.CommentReason');
            }
            CommentReasonRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = CommentReasonRow.Fields || (CommentReasonRow.Fields = {}));
            ['Id', 'Comment', 'CreatedUserId', 'CreatedDate'].forEach(function (x) { return Fields[x] = x; });
        })(CommentReasonRow = Case.CommentReasonRow || (Case.CommentReasonRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CommentReasonService;
        (function (CommentReasonService) {
            CommentReasonService.baseUrl = 'Case/CommentReason';
            var Methods;
            (function (Methods) {
            })(Methods = CommentReasonService.Methods || (CommentReasonService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                CommentReasonService[x] = function (r, s, o) { return Q.serviceRequest(CommentReasonService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = CommentReasonService.baseUrl + '/' + x;
            });
        })(CommentReasonService = Case.CommentReasonService || (Case.CommentReasonService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CompanyForm = (function (_super) {
            __extends(CompanyForm, _super);
            function CompanyForm() {
                _super.apply(this, arguments);
            }
            CompanyForm.formKey = 'Case.Company';
            return CompanyForm;
        }(Serenity.PrefixedContext));
        Case.CompanyForm = CompanyForm;
        [['Name', function () { return Serenity.StringEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['ModifiedUserId', function () { return Serenity.IntegerEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.IntegerEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(CompanyForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CompanyRow;
        (function (CompanyRow) {
            CompanyRow.idProperty = 'Id';
            CompanyRow.nameProperty = 'Name';
            CompanyRow.localTextPrefix = 'Case.Company';
            var Fields;
            (function (Fields) {
            })(Fields = CompanyRow.Fields || (CompanyRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(CompanyRow = Case.CompanyRow || (Case.CompanyRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CompanyService;
        (function (CompanyService) {
            CompanyService.baseUrl = 'Case/Company';
            var Methods;
            (function (Methods) {
            })(Methods = CompanyService.Methods || (CompanyService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                CompanyService[x] = function (r, s, o) { return Q.serviceRequest(CompanyService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = CompanyService.baseUrl + '/' + x;
            });
        })(CompanyService = Case.CompanyService || (Case.CompanyService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        (function (ConfirmType) {
            ConfirmType[ConfirmType["Technical"] = 1] = "Technical";
            ConfirmType[ConfirmType["Financial"] = 2] = "Financial";
        })(Case.ConfirmType || (Case.ConfirmType = {}));
        var ConfirmType = Case.ConfirmType;
        Serenity.Decorators.registerEnum(ConfirmType, 'Case.ConfirmType');
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CustomerEffectForm = (function (_super) {
            __extends(CustomerEffectForm, _super);
            function CustomerEffectForm() {
                _super.apply(this, arguments);
            }
            CustomerEffectForm.formKey = 'Case.CustomerEffect';
            return CustomerEffectForm;
        }(Serenity.PrefixedContext));
        Case.CustomerEffectForm = CustomerEffectForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(CustomerEffectForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CustomerEffectRow;
        (function (CustomerEffectRow) {
            CustomerEffectRow.idProperty = 'Id';
            CustomerEffectRow.nameProperty = 'Name';
            CustomerEffectRow.localTextPrefix = 'Case.CustomerEffect';
            CustomerEffectRow.lookupKey = 'Case.CustomerEffect';
            function getLookup() {
                return Q.getLookup('Case.CustomerEffect');
            }
            CustomerEffectRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = CustomerEffectRow.Fields || (CustomerEffectRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(CustomerEffectRow = Case.CustomerEffectRow || (Case.CustomerEffectRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CustomerEffectService;
        (function (CustomerEffectService) {
            CustomerEffectService.baseUrl = 'Case/CustomerEffect';
            var Methods;
            (function (Methods) {
            })(Methods = CustomerEffectService.Methods || (CustomerEffectService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                CustomerEffectService[x] = function (r, s, o) { return Q.serviceRequest(CustomerEffectService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = CustomerEffectService.baseUrl + '/' + x;
            });
        })(CustomerEffectService = Case.CustomerEffectService || (Case.CustomerEffectService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CycleForm = (function (_super) {
            __extends(CycleForm, _super);
            function CycleForm() {
                _super.apply(this, arguments);
            }
            CycleForm.formKey = 'Case.Cycle';
            return CycleForm;
        }(Serenity.PrefixedContext));
        Case.CycleForm = CycleForm;
        [['YearId', function () { return Serenity.LookupEditor; }], ['Cycle', function () { return Serenity.IntegerEditor; }], ['IsEnabled', function () { return Serenity.BooleanEditor; }]].forEach(function (x) { return Object.defineProperty(CycleForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CycleRow;
        (function (CycleRow) {
            CycleRow.idProperty = 'Id';
            CycleRow.nameProperty = 'CycleName';
            CycleRow.localTextPrefix = 'Case.Cycle';
            CycleRow.lookupKey = 'Case.Cycle';
            function getLookup() {
                return Q.getLookup('Case.Cycle');
            }
            CycleRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = CycleRow.Fields || (CycleRow.Fields = {}));
            ['Id', 'YearId', 'Cycle', 'CycleName', 'IsEnabled', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'Year'].forEach(function (x) { return Fields[x] = x; });
        })(CycleRow = Case.CycleRow || (Case.CycleRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var CycleService;
        (function (CycleService) {
            CycleService.baseUrl = 'Case/Cycle';
            var Methods;
            (function (Methods) {
            })(Methods = CycleService.Methods || (CycleService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                CycleService[x] = function (r, s, o) { return Q.serviceRequest(CycleService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = CycleService.baseUrl + '/' + x;
            });
        })(CycleService = Case.CycleService || (Case.CycleService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var IncomeFlowForm = (function (_super) {
            __extends(IncomeFlowForm, _super);
            function IncomeFlowForm() {
                _super.apply(this, arguments);
            }
            IncomeFlowForm.formKey = 'Case.IncomeFlow';
            return IncomeFlowForm;
        }(Serenity.PrefixedContext));
        Case.IncomeFlowForm = IncomeFlowForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(IncomeFlowForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var IncomeFlowRow;
        (function (IncomeFlowRow) {
            IncomeFlowRow.idProperty = 'Id';
            IncomeFlowRow.nameProperty = 'Name';
            IncomeFlowRow.localTextPrefix = 'Case.IncomeFlow';
            IncomeFlowRow.lookupKey = 'Case.IncomeFlow';
            function getLookup() {
                return Q.getLookup('Case.IncomeFlow');
            }
            IncomeFlowRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = IncomeFlowRow.Fields || (IncomeFlowRow.Fields = {}));
            ['Id', 'Name', 'Code', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(IncomeFlowRow = Case.IncomeFlowRow || (Case.IncomeFlowRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var IncomeFlowService;
        (function (IncomeFlowService) {
            IncomeFlowService.baseUrl = 'Case/IncomeFlow';
            var Methods;
            (function (Methods) {
            })(Methods = IncomeFlowService.Methods || (IncomeFlowService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                IncomeFlowService[x] = function (r, s, o) { return Q.serviceRequest(IncomeFlowService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = IncomeFlowService.baseUrl + '/' + x;
            });
        })(IncomeFlowService = Case.IncomeFlowService || (Case.IncomeFlowService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var PmoLevelForm = (function (_super) {
            __extends(PmoLevelForm, _super);
            function PmoLevelForm() {
                _super.apply(this, arguments);
            }
            PmoLevelForm.formKey = 'Case.PmoLevel';
            return PmoLevelForm;
        }(Serenity.PrefixedContext));
        Case.PmoLevelForm = PmoLevelForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(PmoLevelForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var PmoLevelRow;
        (function (PmoLevelRow) {
            PmoLevelRow.idProperty = 'Id';
            PmoLevelRow.nameProperty = 'Name';
            PmoLevelRow.localTextPrefix = 'Case.PmoLevel';
            PmoLevelRow.lookupKey = 'Case.PmoLevel';
            function getLookup() {
                return Q.getLookup('Case.PmoLevel');
            }
            PmoLevelRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = PmoLevelRow.Fields || (PmoLevelRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(PmoLevelRow = Case.PmoLevelRow || (Case.PmoLevelRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var PmoLevelService;
        (function (PmoLevelService) {
            PmoLevelService.baseUrl = 'Case/PmoLevel';
            var Methods;
            (function (Methods) {
            })(Methods = PmoLevelService.Methods || (PmoLevelService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                PmoLevelService[x] = function (r, s, o) { return Q.serviceRequest(PmoLevelService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = PmoLevelService.baseUrl + '/' + x;
            });
        })(PmoLevelService = Case.PmoLevelService || (Case.PmoLevelService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceCompanySoftwareForm = (function (_super) {
            __extends(ProvinceCompanySoftwareForm, _super);
            function ProvinceCompanySoftwareForm() {
                _super.apply(this, arguments);
            }
            ProvinceCompanySoftwareForm.formKey = 'Case.ProvinceCompanySoftware';
            return ProvinceCompanySoftwareForm;
        }(Serenity.PrefixedContext));
        Case.ProvinceCompanySoftwareForm = ProvinceCompanySoftwareForm;
        [['ProvinveId', function () { return Serenity.IntegerEditor; }], ['CompanyId', function () { return Serenity.IntegerEditor; }], ['SoftwareId', function () { return Serenity.IntegerEditor; }], ['StatusId', function () { return Serenity.EnumEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['ModifiedUserId', function () { return Serenity.IntegerEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.IntegerEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(ProvinceCompanySoftwareForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceCompanySoftwareRow;
        (function (ProvinceCompanySoftwareRow) {
            ProvinceCompanySoftwareRow.idProperty = 'Id';
            ProvinceCompanySoftwareRow.localTextPrefix = 'Case.ProvinceCompanySoftware';
            var Fields;
            (function (Fields) {
            })(Fields = ProvinceCompanySoftwareRow.Fields || (ProvinceCompanySoftwareRow.Fields = {}));
            ['Id', 'ProvinveId', 'CompanyId', 'SoftwareId', 'StatusID', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinveName', 'CompanyName', 'SoftwareName'].forEach(function (x) { return Fields[x] = x; });
        })(ProvinceCompanySoftwareRow = Case.ProvinceCompanySoftwareRow || (Case.ProvinceCompanySoftwareRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceCompanySoftwareService;
        (function (ProvinceCompanySoftwareService) {
            ProvinceCompanySoftwareService.baseUrl = 'Case/ProvinceCompanySoftware';
            var Methods;
            (function (Methods) {
            })(Methods = ProvinceCompanySoftwareService.Methods || (ProvinceCompanySoftwareService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ProvinceCompanySoftwareService[x] = function (r, s, o) { return Q.serviceRequest(ProvinceCompanySoftwareService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ProvinceCompanySoftwareService.baseUrl + '/' + x;
            });
        })(ProvinceCompanySoftwareService = Case.ProvinceCompanySoftwareService || (Case.ProvinceCompanySoftwareService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceForm = (function (_super) {
            __extends(ProvinceForm, _super);
            function ProvinceForm() {
                _super.apply(this, arguments);
            }
            ProvinceForm.formKey = 'Case.Province';
            return ProvinceForm;
        }(Serenity.PrefixedContext));
        Case.ProvinceForm = ProvinceForm;
        [['Name', function () { return Serenity.TextAreaEditor; }], ['ManagerName', function () { return Serenity.StringEditor; }], ['Code', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ProvinceForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramForm = (function (_super) {
            __extends(ProvinceProgramForm, _super);
            function ProvinceProgramForm() {
                _super.apply(this, arguments);
            }
            ProvinceProgramForm.formKey = 'Case.ProvinceProgram';
            return ProvinceProgramForm;
        }(Serenity.PrefixedContext));
        Case.ProvinceProgramForm = ProvinceProgramForm;
        [['Program', function () { return Serenity.StringEditor; }], ['YearId', function () { return Serenity.LookupEditor; }]].forEach(function (x) { return Object.defineProperty(ProvinceProgramForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramLogForm = (function (_super) {
            __extends(ProvinceProgramLogForm, _super);
            function ProvinceProgramLogForm() {
                _super.apply(this, arguments);
            }
            ProvinceProgramLogForm.formKey = 'Case.ProvinceProgramLog';
            return ProvinceProgramLogForm;
        }(Serenity.PrefixedContext));
        Case.ProvinceProgramLogForm = ProvinceProgramLogForm;
        [['ProvinceId', function () { return Serenity.IntegerEditor; }], ['YearId', function () { return Serenity.IntegerEditor; }], ['OldTotalLeakage', function () { return Serenity.StringEditor; }], ['NewTotalLeakage', function () { return Serenity.StringEditor; }], ['OldRecoverableLeakage', function () { return Serenity.StringEditor; }], ['NewRecoverableLeakage', function () { return Serenity.StringEditor; }], ['OldRecovered', function () { return Serenity.StringEditor; }], ['NewRecovered', function () { return Serenity.StringEditor; }], ['UserId', function () { return Serenity.IntegerEditor; }], ['InsertDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(ProvinceProgramLogForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramLogRow;
        (function (ProvinceProgramLogRow) {
            ProvinceProgramLogRow.idProperty = 'Id';
            ProvinceProgramLogRow.localTextPrefix = 'Case.ProvinceProgramLog';
            var Fields;
            (function (Fields) {
            })(Fields = ProvinceProgramLogRow.Fields || (ProvinceProgramLogRow.Fields = {}));
            ['Id', 'ActivityRequestID', 'ProvinceId', 'YearId', 'OldTotalLeakage', 'NewTotalLeakage', 'OldRecoverableLeakage', 'NewRecoverableLeakage', 'OldRecovered', 'NewRecovered', 'UserId', 'InsertDate'].forEach(function (x) { return Fields[x] = x; });
        })(ProvinceProgramLogRow = Case.ProvinceProgramLogRow || (Case.ProvinceProgramLogRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramLogService;
        (function (ProvinceProgramLogService) {
            ProvinceProgramLogService.baseUrl = 'Case/ProvinceProgramLog';
            var Methods;
            (function (Methods) {
            })(Methods = ProvinceProgramLogService.Methods || (ProvinceProgramLogService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ProvinceProgramLogService[x] = function (r, s, o) { return Q.serviceRequest(ProvinceProgramLogService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ProvinceProgramLogService.baseUrl + '/' + x;
            });
        })(ProvinceProgramLogService = Case.ProvinceProgramLogService || (Case.ProvinceProgramLogService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramRow;
        (function (ProvinceProgramRow) {
            ProvinceProgramRow.idProperty = 'Id';
            ProvinceProgramRow.localTextPrefix = 'Case.ProvinceProgram';
            var Fields;
            (function (Fields) {
            })(Fields = ProvinceProgramRow.Fields || (ProvinceProgramRow.Fields = {}));
            ['Id', 'Program', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'PercentTotalLeakage', 'PercentRecoverableLeakage', 'PercentRecovered', 'PercentRecoveredonTotal', 'PercentTotal94to95', 'PercentRecovered94to95', 'ActivityCount', 'ActivityNonRepeatCount', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'LastActivityDate', 'ProvinceId', 'YearId', 'ProvinceName', 'Year'].forEach(function (x) { return Fields[x] = x; });
        })(ProvinceProgramRow = Case.ProvinceProgramRow || (Case.ProvinceProgramRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceProgramService;
        (function (ProvinceProgramService) {
            ProvinceProgramService.baseUrl = 'Case/ProvinceProgram';
            var Methods;
            (function (Methods) {
            })(Methods = ProvinceProgramService.Methods || (ProvinceProgramService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'ProvinceProgramLineReport96', 'ProvinceProgramLineReport', 'ProvinceProgramLineReport94', 'ProvinceProgramLineReport93', 'ProvinceProgramLineReport92', 'LeakProgramReport95', 'ConfirmProgramReport95', 'LeakConfirmReport95'].forEach(function (x) {
                ProvinceProgramService[x] = function (r, s, o) { return Q.serviceRequest(ProvinceProgramService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ProvinceProgramService.baseUrl + '/' + x;
            });
        })(ProvinceProgramService = Case.ProvinceProgramService || (Case.ProvinceProgramService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceRow;
        (function (ProvinceRow) {
            ProvinceRow.idProperty = 'Id';
            ProvinceRow.nameProperty = 'Name';
            ProvinceRow.localTextPrefix = 'Case.Province';
            ProvinceRow.lookupKey = 'Case.Province';
            function getLookup() {
                return Q.getLookup('Case.Province');
            }
            ProvinceRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = ProvinceRow.Fields || (ProvinceRow.Fields = {}));
            ['Id', 'LeaderID', 'Name', 'Code', 'EnglishName', 'ManagerName', 'LetterNo', 'PmoLevelId', 'InstallDate', 'PmoLevelName', 'LeaderName'].forEach(function (x) { return Fields[x] = x; });
        })(ProvinceRow = Case.ProvinceRow || (Case.ProvinceRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var ProvinceService;
        (function (ProvinceService) {
            ProvinceService.baseUrl = 'Case/Province';
            var Methods;
            (function (Methods) {
            })(Methods = ProvinceService.Methods || (ProvinceService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ProvinceService[x] = function (r, s, o) { return Q.serviceRequest(ProvinceService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ProvinceService.baseUrl + '/' + x;
            });
        })(ProvinceService = Case.ProvinceService || (Case.ProvinceService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RepeatTermForm = (function (_super) {
            __extends(RepeatTermForm, _super);
            function RepeatTermForm() {
                _super.apply(this, arguments);
            }
            RepeatTermForm.formKey = 'Case.RepeatTerm';
            return RepeatTermForm;
        }(Serenity.PrefixedContext));
        Case.RepeatTermForm = RepeatTermForm;
        [['Name', function () { return Serenity.StringEditor; }], ['RequiredYearRepeatCount', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(RepeatTermForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RepeatTermRow;
        (function (RepeatTermRow) {
            RepeatTermRow.idProperty = 'Id';
            RepeatTermRow.nameProperty = 'Name';
            RepeatTermRow.localTextPrefix = 'Case.RepeatTerm';
            RepeatTermRow.lookupKey = 'Case.RepeatTerm';
            function getLookup() {
                return Q.getLookup('Case.RepeatTerm');
            }
            RepeatTermRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = RepeatTermRow.Fields || (RepeatTermRow.Fields = {}));
            ['Id', 'Name', 'DayValue', 'RequiredYearRepeatCount', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(RepeatTermRow = Case.RepeatTermRow || (Case.RepeatTermRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RepeatTermService;
        (function (RepeatTermService) {
            RepeatTermService.baseUrl = 'Case/RepeatTerm';
            var Methods;
            (function (Methods) {
            })(Methods = RepeatTermService.Methods || (RepeatTermService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                RepeatTermService[x] = function (r, s, o) { return Q.serviceRequest(RepeatTermService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = RepeatTermService.baseUrl + '/' + x;
            });
        })(RepeatTermService = Case.RepeatTermService || (Case.RepeatTermService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        (function (RequestAction) {
            RequestAction[RequestAction["Save"] = 1] = "Save";
            RequestAction[RequestAction["Forward"] = 2] = "Forward";
            RequestAction[RequestAction["Deny"] = 3] = "Deny";
            RequestAction[RequestAction["Delete"] = 4] = "Delete";
        })(Case.RequestAction || (Case.RequestAction = {}));
        var RequestAction = Case.RequestAction;
        Serenity.Decorators.registerEnum(RequestAction, 'Case.RequestAction');
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        (function (RequestActionAdmin) {
            RequestActionAdmin[RequestActionAdmin["Deny"] = 3] = "Deny";
            RequestActionAdmin[RequestActionAdmin["Delete"] = 4] = "Delete";
        })(Case.RequestActionAdmin || (Case.RequestActionAdmin = {}));
        var RequestActionAdmin = Case.RequestActionAdmin;
        Serenity.Decorators.registerEnum(RequestActionAdmin, 'Case.RequestActionAdmin');
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RiskLevelForm = (function (_super) {
            __extends(RiskLevelForm, _super);
            function RiskLevelForm() {
                _super.apply(this, arguments);
            }
            RiskLevelForm.formKey = 'Case.RiskLevel';
            return RiskLevelForm;
        }(Serenity.PrefixedContext));
        Case.RiskLevelForm = RiskLevelForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(RiskLevelForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RiskLevelRow;
        (function (RiskLevelRow) {
            RiskLevelRow.idProperty = 'Id';
            RiskLevelRow.nameProperty = 'Name';
            RiskLevelRow.localTextPrefix = 'Case.RiskLevel';
            RiskLevelRow.lookupKey = 'Case.RiskLevel';
            function getLookup() {
                return Q.getLookup('Case.RiskLevel');
            }
            RiskLevelRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = RiskLevelRow.Fields || (RiskLevelRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(RiskLevelRow = Case.RiskLevelRow || (Case.RiskLevelRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var RiskLevelService;
        (function (RiskLevelService) {
            RiskLevelService.baseUrl = 'Case/RiskLevel';
            var Methods;
            (function (Methods) {
            })(Methods = RiskLevelService.Methods || (RiskLevelService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                RiskLevelService[x] = function (r, s, o) { return Q.serviceRequest(RiskLevelService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = RiskLevelService.baseUrl + '/' + x;
            });
        })(RiskLevelService = Case.RiskLevelService || (Case.RiskLevelService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SMSLogForm = (function (_super) {
            __extends(SMSLogForm, _super);
            function SMSLogForm() {
                _super.apply(this, arguments);
            }
            SMSLogForm.formKey = 'Case.SMSLog';
            return SMSLogForm;
        }(Serenity.PrefixedContext));
        Case.SMSLogForm = SMSLogForm;
        [['ActivityRequestId', function () { return Serenity.StringEditor; }], ['ReceiverProvinceId', function () { return Serenity.LookupEditor; }], ['ReceiverUserName', function () { return Serenity.StringEditor; }], ['TextSent', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(SMSLogForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SMSLogRow;
        (function (SMSLogRow) {
            SMSLogRow.idProperty = 'Id';
            SMSLogRow.nameProperty = 'SenderUserName';
            SMSLogRow.localTextPrefix = 'Case.SMSLog';
            SMSLogRow.lookupKey = 'Case.SMSLogRow';
            function getLookup() {
                return Q.getLookup('Case.SMSLogRow');
            }
            SMSLogRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = SMSLogRow.Fields || (SMSLogRow.Fields = {}));
            ['Id', 'Is_modified', 'InsertDate', 'ActivityRequestId', 'MessageId', 'SenderUserId', 'SenderUserName', 'ReceiverProvinceId', 'ReceiverUserId', 'ReceiverUserName', 'MobileNumber', 'TextSent', 'IsSent', 'IsDelivered', 'ReceiverRoleId', 'ReceiverProvinceLeaderId', 'ReceiverProvinceName', 'ReceiverProvinceCode', 'ReceiverProvinceEnglishName', 'ReceiverProvinceManagerName', 'ReceiverProvinceLetterNo', 'ReceiverProvincePmoLevelId', 'ReceiverProvinceInstallDate', 'ModifiedDate', 'ReceiverRoleRoleName'].forEach(function (x) { return Fields[x] = x; });
        })(SMSLogRow = Case.SMSLogRow || (Case.SMSLogRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SMSLogService;
        (function (SMSLogService) {
            SMSLogService.baseUrl = 'Case/SMSLog';
            var Methods;
            (function (Methods) {
            })(Methods = SMSLogService.Methods || (SMSLogService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SMSLogService[x] = function (r, s, o) { return Q.serviceRequest(SMSLogService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SMSLogService.baseUrl + '/' + x;
            });
        })(SMSLogService = Case.SMSLogService || (Case.SMSLogService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SoftwareForm = (function (_super) {
            __extends(SoftwareForm, _super);
            function SoftwareForm() {
                _super.apply(this, arguments);
            }
            SoftwareForm.formKey = 'Case.Software';
            return SoftwareForm;
        }(Serenity.PrefixedContext));
        Case.SoftwareForm = SoftwareForm;
        [['Name', function () { return Serenity.StringEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['ModifiedUserId', function () { return Serenity.IntegerEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.IntegerEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(SoftwareForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SoftwareRow;
        (function (SoftwareRow) {
            SoftwareRow.idProperty = 'Id';
            SoftwareRow.nameProperty = 'Name';
            SoftwareRow.localTextPrefix = 'Case.Software';
            var Fields;
            (function (Fields) {
            })(Fields = SoftwareRow.Fields || (SoftwareRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(SoftwareRow = Case.SoftwareRow || (Case.SoftwareRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SoftwareService;
        (function (SoftwareService) {
            SoftwareService.baseUrl = 'Case/Software';
            var Methods;
            (function (Methods) {
            })(Methods = SoftwareService.Methods || (SoftwareService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SoftwareService[x] = function (r, s, o) { return Q.serviceRequest(SoftwareService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SoftwareService.baseUrl + '/' + x;
            });
        })(SoftwareService = Case.SoftwareService || (Case.SoftwareService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        (function (SoftwareStatus) {
            SoftwareStatus[SoftwareStatus["Yes"] = 1] = "Yes";
            SoftwareStatus[SoftwareStatus["No"] = 2] = "No";
            SoftwareStatus[SoftwareStatus["Pendding"] = 3] = "Pendding";
        })(Case.SoftwareStatus || (Case.SoftwareStatus = {}));
        var SoftwareStatus = Case.SoftwareStatus;
        Serenity.Decorators.registerEnum(SoftwareStatus, 'Case.SoftwareStatus');
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamForm = (function (_super) {
            __extends(SwitchDslamForm, _super);
            function SwitchDslamForm() {
                _super.apply(this, arguments);
            }
            SwitchDslamForm.formKey = 'Case.SwitchDslam';
            return SwitchDslamForm;
        }(Serenity.PrefixedContext));
        Case.SwitchDslamForm = SwitchDslamForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(SwitchDslamForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamProvinceForm = (function (_super) {
            __extends(SwitchDslamProvinceForm, _super);
            function SwitchDslamProvinceForm() {
                _super.apply(this, arguments);
            }
            SwitchDslamProvinceForm.formKey = 'Case.SwitchDslamProvince';
            return SwitchDslamProvinceForm;
        }(Serenity.PrefixedContext));
        Case.SwitchDslamProvinceForm = SwitchDslamProvinceForm;
        [['ProvinceId', function () { return Serenity.IntegerEditor; }], ['SwitchDslamid', function () { return Serenity.IntegerEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['ModifiedUserId', function () { return Serenity.IntegerEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.IntegerEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(SwitchDslamProvinceForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamProvinceRow;
        (function (SwitchDslamProvinceRow) {
            SwitchDslamProvinceRow.idProperty = 'Id';
            SwitchDslamProvinceRow.localTextPrefix = 'Case.SwitchDslamProvince';
            var Fields;
            (function (Fields) {
            })(Fields = SwitchDslamProvinceRow.Fields || (SwitchDslamProvinceRow.Fields = {}));
            ['Id', 'ProvinceId', 'SwitchDslamid', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinceName', 'SwitchDslamidName'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchDslamProvinceRow = Case.SwitchDslamProvinceRow || (Case.SwitchDslamProvinceRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamProvinceService;
        (function (SwitchDslamProvinceService) {
            SwitchDslamProvinceService.baseUrl = 'Case/SwitchDslamProvince';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchDslamProvinceService.Methods || (SwitchDslamProvinceService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchDslamProvinceService[x] = function (r, s, o) { return Q.serviceRequest(SwitchDslamProvinceService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchDslamProvinceService.baseUrl + '/' + x;
            });
        })(SwitchDslamProvinceService = Case.SwitchDslamProvinceService || (Case.SwitchDslamProvinceService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamRow;
        (function (SwitchDslamRow) {
            SwitchDslamRow.idProperty = 'Id';
            SwitchDslamRow.nameProperty = 'Name';
            SwitchDslamRow.localTextPrefix = 'Case.SwitchDslam';
            var Fields;
            (function (Fields) {
            })(Fields = SwitchDslamRow.Fields || (SwitchDslamRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchDslamRow = Case.SwitchDslamRow || (Case.SwitchDslamRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchDslamService;
        (function (SwitchDslamService) {
            SwitchDslamService.baseUrl = 'Case/SwitchDslam';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchDslamService.Methods || (SwitchDslamService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchDslamService[x] = function (r, s, o) { return Q.serviceRequest(SwitchDslamService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchDslamService.baseUrl + '/' + x;
            });
        })(SwitchDslamService = Case.SwitchDslamService || (Case.SwitchDslamService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchForm = (function (_super) {
            __extends(SwitchForm, _super);
            function SwitchForm() {
                _super.apply(this, arguments);
            }
            SwitchForm.formKey = 'Case.Switch';
            return SwitchForm;
        }(Serenity.PrefixedContext));
        Case.SwitchForm = SwitchForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(SwitchForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchProvinceRow;
        (function (SwitchProvinceRow) {
            SwitchProvinceRow.idProperty = 'Id';
            SwitchProvinceRow.localTextPrefix = 'Case.SwitchProvince';
            var Fields;
            (function (Fields) {
            })(Fields = SwitchProvinceRow.Fields || (SwitchProvinceRow.Fields = {}));
            ['Id', 'ProvinceId', 'SwitchId', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinceName', 'SwitchName'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchProvinceRow = Case.SwitchProvinceRow || (Case.SwitchProvinceRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchProvinceService;
        (function (SwitchProvinceService) {
            SwitchProvinceService.baseUrl = 'Case/SwitchProvince';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchProvinceService.Methods || (SwitchProvinceService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchProvinceService[x] = function (r, s, o) { return Q.serviceRequest(SwitchProvinceService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchProvinceService.baseUrl + '/' + x;
            });
        })(SwitchProvinceService = Case.SwitchProvinceService || (Case.SwitchProvinceService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchRow;
        (function (SwitchRow) {
            SwitchRow.idProperty = 'Id';
            SwitchRow.nameProperty = 'Name';
            SwitchRow.localTextPrefix = 'Case.Switch';
            var Fields;
            (function (Fields) {
            })(Fields = SwitchRow.Fields || (SwitchRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchRow = Case.SwitchRow || (Case.SwitchRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchService;
        (function (SwitchService) {
            SwitchService.baseUrl = 'Case/Switch';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchService.Methods || (SwitchService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchService[x] = function (r, s, o) { return Q.serviceRequest(SwitchService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchService.baseUrl + '/' + x;
            });
        })(SwitchService = Case.SwitchService || (Case.SwitchService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitForm = (function (_super) {
            __extends(SwitchTransitForm, _super);
            function SwitchTransitForm() {
                _super.apply(this, arguments);
            }
            SwitchTransitForm.formKey = 'Case.SwitchTransit';
            return SwitchTransitForm;
        }(Serenity.PrefixedContext));
        Case.SwitchTransitForm = SwitchTransitForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(SwitchTransitForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitProvinceForm = (function (_super) {
            __extends(SwitchTransitProvinceForm, _super);
            function SwitchTransitProvinceForm() {
                _super.apply(this, arguments);
            }
            SwitchTransitProvinceForm.formKey = 'Case.SwitchTransitProvince';
            return SwitchTransitProvinceForm;
        }(Serenity.PrefixedContext));
        Case.SwitchTransitProvinceForm = SwitchTransitProvinceForm;
        [['ProvinceId', function () { return Serenity.LookupEditor; }], ['SwitchTransitId', function () { return Serenity.LookupEditor; }]].forEach(function (x) { return Object.defineProperty(SwitchTransitProvinceForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitProvinceRow;
        (function (SwitchTransitProvinceRow) {
            SwitchTransitProvinceRow.idProperty = 'Id';
            SwitchTransitProvinceRow.nameProperty = 'ProvinceName';
            SwitchTransitProvinceRow.localTextPrefix = 'Case.SwitchTransitProvince';
            var Fields;
            (function (Fields) {
            })(Fields = SwitchTransitProvinceRow.Fields || (SwitchTransitProvinceRow.Fields = {}));
            ['Id', 'ProvinceId', 'SwitchTransitId', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ProvinceName', 'SwitchTransitName'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchTransitProvinceRow = Case.SwitchTransitProvinceRow || (Case.SwitchTransitProvinceRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitProvinceService;
        (function (SwitchTransitProvinceService) {
            SwitchTransitProvinceService.baseUrl = 'Case/SwitchTransitProvince';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchTransitProvinceService.Methods || (SwitchTransitProvinceService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchTransitProvinceService[x] = function (r, s, o) { return Q.serviceRequest(SwitchTransitProvinceService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchTransitProvinceService.baseUrl + '/' + x;
            });
        })(SwitchTransitProvinceService = Case.SwitchTransitProvinceService || (Case.SwitchTransitProvinceService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitRow;
        (function (SwitchTransitRow) {
            SwitchTransitRow.idProperty = 'Id';
            SwitchTransitRow.nameProperty = 'Name';
            SwitchTransitRow.localTextPrefix = 'Case.SwitchTransit';
            SwitchTransitRow.lookupKey = 'Case.SwitchTransit';
            function getLookup() {
                return Q.getLookup('Case.SwitchTransit');
            }
            SwitchTransitRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = SwitchTransitRow.Fields || (SwitchTransitRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(SwitchTransitRow = Case.SwitchTransitRow || (Case.SwitchTransitRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var SwitchTransitService;
        (function (SwitchTransitService) {
            SwitchTransitService.baseUrl = 'Case/SwitchTransit';
            var Methods;
            (function (Methods) {
            })(Methods = SwitchTransitService.Methods || (SwitchTransitService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SwitchTransitService[x] = function (r, s, o) { return Q.serviceRequest(SwitchTransitService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SwitchTransitService.baseUrl + '/' + x;
            });
        })(SwitchTransitService = Case.SwitchTransitService || (Case.SwitchTransitService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var UserProvinceRow;
        (function (UserProvinceRow) {
            UserProvinceRow.idProperty = 'Id';
            UserProvinceRow.localTextPrefix = 'Case.UserProvince';
            var Fields;
            (function (Fields) {
            })(Fields = UserProvinceRow.Fields || (UserProvinceRow.Fields = {}));
            ['Id', 'UserId', 'ProvinceId', 'CreatedUserId', 'CreatedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'UserUsername', 'UserDisplayName', 'UserEmail', 'UserSource', 'UserPasswordHash', 'UserPasswordSalt', 'UserInsertDate', 'UserInsertUserId', 'UserUpdateDate', 'UserUpdateUserId', 'UserIsActive', 'UserLastDirectoryUpdate', 'ProvinceName', 'ProvinceEnglishName', 'ProvinceManagerName', 'ProvinceLetterNo', 'ProvincePmoLevelId', 'ProvinceInstallDate', 'ProvinceCreatedUserId', 'ProvinceCreatedDate', 'ProvinceModifiedUserId', 'ProvinceModifiedDate', 'ProvinceIsDeleted', 'ProvinceDeletedUserId', 'ProvinceDeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(UserProvinceRow = Case.UserProvinceRow || (Case.UserProvinceRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var YearForm = (function (_super) {
            __extends(YearForm, _super);
            function YearForm() {
                _super.apply(this, arguments);
            }
            YearForm.formKey = 'Case.Year';
            return YearForm;
        }(Serenity.PrefixedContext));
        Case.YearForm = YearForm;
        [['Year', function () { return Serenity.IntegerEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['ModifiedUserId', function () { return Serenity.IntegerEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.IntegerEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }]].forEach(function (x) { return Object.defineProperty(YearForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var YearRow;
        (function (YearRow) {
            YearRow.idProperty = 'Id';
            YearRow.nameProperty = 'Year';
            YearRow.localTextPrefix = 'Case.Year';
            YearRow.lookupKey = 'Case.Year';
            function getLookup() {
                return Q.getLookup('Case.Year');
            }
            YearRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = YearRow.Fields || (YearRow.Fields = {}));
            ['Id', 'Year', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(YearRow = Case.YearRow || (Case.YearRow = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Case;
    (function (Case) {
        var YearService;
        (function (YearService) {
            YearService.baseUrl = 'Case/Year';
            var Methods;
            (function (Methods) {
            })(Methods = YearService.Methods || (YearService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                YearService[x] = function (r, s, o) { return Q.serviceRequest(YearService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = YearService.baseUrl + '/' + x;
            });
        })(YearService = Case.YearService || (Case.YearService = {}));
    })(Case = CaseManagement.Case || (CaseManagement.Case = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var UserPreferenceRow;
        (function (UserPreferenceRow) {
            UserPreferenceRow.idProperty = 'UserPreferenceId';
            UserPreferenceRow.nameProperty = 'Name';
            UserPreferenceRow.localTextPrefix = 'Common.UserPreference';
            var Fields;
            (function (Fields) {
            })(Fields = UserPreferenceRow.Fields || (UserPreferenceRow.Fields = {}));
            ['UserPreferenceId', 'UserId', 'PreferenceType', 'Name', 'Value'].forEach(function (x) { return Fields[x] = x; });
        })(UserPreferenceRow = Common.UserPreferenceRow || (Common.UserPreferenceRow = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var UserPreferenceService;
        (function (UserPreferenceService) {
            UserPreferenceService.baseUrl = 'Common/UserPreference';
            var Methods;
            (function (Methods) {
            })(Methods = UserPreferenceService.Methods || (UserPreferenceService.Methods = {}));
            ['Update', 'Retrieve'].forEach(function (x) {
                UserPreferenceService[x] = function (r, s, o) { return Q.serviceRequest(UserPreferenceService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = UserPreferenceService.baseUrl + '/' + x;
            });
        })(UserPreferenceService = Common.UserPreferenceService || (Common.UserPreferenceService = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ChangePasswordForm = (function (_super) {
            __extends(ChangePasswordForm, _super);
            function ChangePasswordForm() {
                _super.apply(this, arguments);
            }
            ChangePasswordForm.formKey = 'Membership.ChangePassword';
            return ChangePasswordForm;
        }(Serenity.PrefixedContext));
        Membership.ChangePasswordForm = ChangePasswordForm;
        [['OldPassword', function () { return Serenity.PasswordEditor; }], ['NewPassword', function () { return Serenity.PasswordEditor; }], ['ConfirmPassword', function () { return Serenity.PasswordEditor; }]].forEach(function (x) { return Object.defineProperty(ChangePasswordForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ForgotPasswordForm = (function (_super) {
            __extends(ForgotPasswordForm, _super);
            function ForgotPasswordForm() {
                _super.apply(this, arguments);
            }
            ForgotPasswordForm.formKey = 'Membership.ForgotPassword';
            return ForgotPasswordForm;
        }(Serenity.PrefixedContext));
        Membership.ForgotPasswordForm = ForgotPasswordForm;
        [['Email', function () { return Serenity.EmailEditor; }]].forEach(function (x) { return Object.defineProperty(ForgotPasswordForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var LoginForm = (function (_super) {
            __extends(LoginForm, _super);
            function LoginForm() {
                _super.apply(this, arguments);
            }
            LoginForm.formKey = 'Membership.Login';
            return LoginForm;
        }(Serenity.PrefixedContext));
        Membership.LoginForm = LoginForm;
        [['Username', function () { return Serenity.StringEditor; }], ['Password', function () { return Serenity.PasswordEditor; }]].forEach(function (x) { return Object.defineProperty(LoginForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ResetPasswordForm = (function (_super) {
            __extends(ResetPasswordForm, _super);
            function ResetPasswordForm() {
                _super.apply(this, arguments);
            }
            ResetPasswordForm.formKey = 'Membership.ResetPassword';
            return ResetPasswordForm;
        }(Serenity.PrefixedContext));
        Membership.ResetPasswordForm = ResetPasswordForm;
        [['NewPassword', function () { return Serenity.PasswordEditor; }], ['ConfirmPassword', function () { return Serenity.PasswordEditor; }]].forEach(function (x) { return Object.defineProperty(ResetPasswordForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var SignUpForm = (function (_super) {
            __extends(SignUpForm, _super);
            function SignUpForm() {
                _super.apply(this, arguments);
            }
            SignUpForm.formKey = 'Membership.SignUp';
            return SignUpForm;
        }(Serenity.PrefixedContext));
        Membership.SignUpForm = SignUpForm;
        [['DisplayName', function () { return Serenity.StringEditor; }], ['Email', function () { return Serenity.EmailEditor; }], ['ConfirmEmail', function () { return Serenity.EmailEditor; }], ['Password', function () { return Serenity.PasswordEditor; }], ['ConfirmPassword', function () { return Serenity.PasswordEditor; }]].forEach(function (x) { return Object.defineProperty(SignUpForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var InboxForm = (function (_super) {
            __extends(InboxForm, _super);
            function InboxForm() {
                _super.apply(this, arguments);
            }
            InboxForm.formKey = 'Messaging.Inbox';
            return InboxForm;
        }(Serenity.PrefixedContext));
        Messaging.InboxForm = InboxForm;
        [['SenderDisplayName', function () { return Serenity.StringEditor; }], ['MessageSubject', function () { return Serenity.TextAreaEditor; }], ['MessageBody', function () { return Serenity.TextAreaEditor; }], ['MessageFile', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(InboxForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var InboxRow;
        (function (InboxRow) {
            InboxRow.idProperty = 'Id';
            InboxRow.localTextPrefix = 'Messaging.Inbox';
            var Fields;
            (function (Fields) {
            })(Fields = InboxRow.Fields || (InboxRow.Fields = {}));
            ['Id', 'MessageId', 'RecieverId', 'SenderId', 'Seen', 'SeenDate', 'MessageSubject', 'MessageBody', 'MessageFile', 'MessageInsertedDate', 'RecieverDisplayName', 'SenderDisplayName'].forEach(function (x) { return Fields[x] = x; });
        })(InboxRow = Messaging.InboxRow || (Messaging.InboxRow = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var InboxService;
        (function (InboxService) {
            InboxService.baseUrl = 'Messaging/Inbox';
            var Methods;
            (function (Methods) {
            })(Methods = InboxService.Methods || (InboxService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                InboxService[x] = function (r, s, o) { return Q.serviceRequest(InboxService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = InboxService.baseUrl + '/' + x;
            });
        })(InboxService = Messaging.InboxService || (Messaging.InboxService = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var MessagesReceiversRow;
        (function (MessagesReceiversRow) {
            MessagesReceiversRow.idProperty = 'Id';
            MessagesReceiversRow.localTextPrefix = 'Messaging.MessagesReceivers';
            var Fields;
            (function (Fields) {
            })(Fields = MessagesReceiversRow.Fields || (MessagesReceiversRow.Fields = {}));
            ['Id', 'MessageId', 'RecieverId', 'SenderId', 'Seen', 'SeenDate', 'MessageSenderId', 'MessageSubject', 'MessageBody', 'MessageFile', 'MessageInsertedDate', 'SenderDisplayName', 'RecieverOldcaseId', 'RecieverUsername', 'RecieverDisplayName', 'RecieverEmployeeId', 'RecieverEmail', 'RecieverRank', 'RecieverSource', 'RecieverPassword', 'RecieverPasswordHash', 'RecieverPasswordSalt', 'RecieverLastLoginDate', 'RecieverInsertDate', 'RecieverInsertUserId', 'RecieverUpdateDate', 'RecieverUpdateUserId', 'RecieverIsActive', 'RecieverLastDirectoryUpdate', 'RecieverRoleId', 'RecieverTelephoneNo1', 'RecieverTelephoneNo2', 'RecieverMobileNo', 'RecieverDegree', 'RecieverProvinceId', 'RecieverIsIranTci', 'RecieverIsDeleted', 'RecieverDeletedUserId', 'RecieverDeletedDate', 'RecieverImagePath'].forEach(function (x) { return Fields[x] = x; });
        })(MessagesReceiversRow = Messaging.MessagesReceiversRow || (Messaging.MessagesReceiversRow = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var NewMessageForm = (function (_super) {
            __extends(NewMessageForm, _super);
            function NewMessageForm() {
                _super.apply(this, arguments);
            }
            NewMessageForm.formKey = 'Messaging.NewMessage';
            return NewMessageForm;
        }(Serenity.PrefixedContext));
        Messaging.NewMessageForm = NewMessageForm;
        [['ReceiverList', function () { return Serenity.LookupEditor; }], ['Subject', function () { return Serenity.TextAreaEditor; }], ['Body', function () { return Serenity.TextAreaEditor; }], ['File', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(NewMessageForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var NewMessageRow;
        (function (NewMessageRow) {
            NewMessageRow.idProperty = 'Id';
            NewMessageRow.nameProperty = 'Subject';
            NewMessageRow.localTextPrefix = 'Messaging.NewMessage';
            NewMessageRow.lookupKey = 'Messaging.NewMessage';
            function getLookup() {
                return Q.getLookup('Messaging.NewMessage');
            }
            NewMessageRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = NewMessageRow.Fields || (NewMessageRow.Fields = {}));
            ['Id', 'SenderId', 'Subject', 'Body', 'File', 'InsertedDate', 'SenderDisplayName', 'ReceiverList'].forEach(function (x) { return Fields[x] = x; });
        })(NewMessageRow = Messaging.NewMessageRow || (Messaging.NewMessageRow = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var NewMessageService;
        (function (NewMessageService) {
            NewMessageService.baseUrl = 'Messaging/NewMessage';
            var Methods;
            (function (Methods) {
            })(Methods = NewMessageService.Methods || (NewMessageService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                NewMessageService[x] = function (r, s, o) { return Q.serviceRequest(NewMessageService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = NewMessageService.baseUrl + '/' + x;
            });
        })(NewMessageService = Messaging.NewMessageService || (Messaging.NewMessageService = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var SentForm = (function (_super) {
            __extends(SentForm, _super);
            function SentForm() {
                _super.apply(this, arguments);
            }
            SentForm.formKey = 'Messaging.Sent';
            return SentForm;
        }(Serenity.PrefixedContext));
        Messaging.SentForm = SentForm;
        [['RecieverDisplayName', function () { return Serenity.StringEditor; }], ['MessageSubject', function () { return Serenity.TextAreaEditor; }], ['MessageBody', function () { return Serenity.TextAreaEditor; }], ['MessageFile', function () { return Serenity.ImageUploadEditor; }]].forEach(function (x) { return Object.defineProperty(SentForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var SentRow;
        (function (SentRow) {
            SentRow.idProperty = 'Id';
            SentRow.localTextPrefix = 'Messaging.Sent';
            var Fields;
            (function (Fields) {
            })(Fields = SentRow.Fields || (SentRow.Fields = {}));
            ['Id', 'MessageId', 'RecieverId', 'SenderId', 'Seen', 'SeenDate', 'MessageSubject', 'MessageBody', 'MessageFile', 'MessageInsertedDate', 'RecieverDisplayName', 'SenderDisplayName'].forEach(function (x) { return Fields[x] = x; });
        })(SentRow = Messaging.SentRow || (Messaging.SentRow = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var SentService;
        (function (SentService) {
            SentService.baseUrl = 'Messaging/Sent';
            var Methods;
            (function (Methods) {
            })(Methods = SentService.Methods || (SentService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                SentService[x] = function (r, s, o) { return Q.serviceRequest(SentService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = SentService.baseUrl + '/' + x;
            });
        })(SentService = Messaging.SentService || (Messaging.SentService = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var VwMessagesRow;
        (function (VwMessagesRow) {
            VwMessagesRow.idProperty = 'Id';
            VwMessagesRow.nameProperty = 'Subject';
            VwMessagesRow.localTextPrefix = 'Messaging.VwMessages';
            var Fields;
            (function (Fields) {
            })(Fields = VwMessagesRow.Fields || (VwMessagesRow.Fields = {}));
            ['Id', 'SenderId', 'Seen', 'SeenDate', 'Subject', 'Body', 'InsertedDate', 'RecieverId', 'MessageId', 'SenderName', 'ReceiverName'].forEach(function (x) { return Fields[x] = x; });
        })(VwMessagesRow = Messaging.VwMessagesRow || (Messaging.VwMessagesRow = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var VwMessagesService;
        (function (VwMessagesService) {
            VwMessagesService.baseUrl = 'Messaging/VwMessages';
            var Methods;
            (function (Methods) {
            })(Methods = VwMessagesService.Methods || (VwMessagesService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                VwMessagesService[x] = function (r, s, o) { return Q.serviceRequest(VwMessagesService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = VwMessagesService.baseUrl + '/' + x;
            });
        })(VwMessagesService = Messaging.VwMessagesService || (Messaging.VwMessagesService = {}));
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var ReportTemplateDB;
(function (ReportTemplateDB) {
    var Common;
    (function (Common) {
        var ReportTemplateRow;
        (function (ReportTemplateRow) {
            ReportTemplateRow.idProperty = 'Id';
            ReportTemplateRow.nameProperty = 'Title';
            ReportTemplateRow.localTextPrefix = 'Common.ReportTemplate';
            var Fields;
            (function (Fields) {
            })(Fields = ReportTemplateRow.Fields || (ReportTemplateRow.Fields = {}));
            ['Id', 'Template', 'Title'].forEach(function (x) { return Fields[x] = x; });
        })(ReportTemplateRow = Common.ReportTemplateRow || (Common.ReportTemplateRow = {}));
    })(Common = ReportTemplateDB.Common || (ReportTemplateDB.Common = {}));
})(ReportTemplateDB || (ReportTemplateDB = {}));
var ReportTemplateDB;
(function (ReportTemplateDB) {
    var Common;
    (function (Common) {
        var ReportTemplateService;
        (function (ReportTemplateService) {
            ReportTemplateService.baseUrl = 'Common/ReportTemplate';
            var Methods;
            (function (Methods) {
            })(Methods = ReportTemplateService.Methods || (ReportTemplateService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ReportTemplateService[x] = function (r, s, o) { return Q.serviceRequest(ReportTemplateService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ReportTemplateService.baseUrl + '/' + x;
            });
        })(ReportTemplateService = Common.ReportTemplateService || (Common.ReportTemplateService = {}));
    })(Common = ReportTemplateDB.Common || (ReportTemplateDB.Common = {}));
})(ReportTemplateDB || (ReportTemplateDB = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityReportForm = (function (_super) {
            __extends(ActivityReportForm, _super);
            function ActivityReportForm() {
                _super.apply(this, arguments);
            }
            ActivityReportForm.formKey = 'StimulReport.ActivityReport';
            return ActivityReportForm;
        }(Serenity.PrefixedContext));
        StimulReport.ActivityReportForm = ActivityReportForm;
        [['Id', function () { return Serenity.IntegerEditor; }], ['RequestId', function () { return Serenity.IntegerEditor; }], ['ProvinceId', function () { return Serenity.IntegerEditor; }], ['ActivityId', function () { return Serenity.IntegerEditor; }], ['DelayCost', function () { return Serenity.IntegerEditor; }], ['YearCost', function () { return Serenity.IntegerEditor; }], ['LeakCost', function () { return Serenity.IntegerEditor; }], ['ConfirmDelayCost', function () { return Serenity.IntegerEditor; }], ['ConfirmYearCost', function () { return Serenity.IntegerEditor; }], ['ConfirmCost', function () { return Serenity.IntegerEditor; }], ['ProgramCost', function () { return Serenity.IntegerEditor; }], ['Percent', function () { return Serenity.StringEditor; }], ['CreatedUserId', function () { return Serenity.IntegerEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDateShamsi', function () { return Serenity.StringEditor; }], ['EndDate', function () { return Serenity.DateEditor; }], ['ConfirmUserId', function () { return Serenity.IntegerEditor; }], ['ActionId', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityReportForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityReportRow;
        (function (ActivityReportRow) {
            ActivityReportRow.idProperty = 'Id';
            ActivityReportRow.nameProperty = 'Percent';
            ActivityReportRow.localTextPrefix = 'StimulReport.ActivityReport';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityReportRow.Fields || (ActivityReportRow.Fields = {}));
            ['Id', 'RequestId', 'ProvinceId', 'ActivityId', 'DelayCost', 'YearCost', 'LeakCost', 'ConfirmDelayCost', 'ConfirmYearCost', 'ConfirmCost', 'ProgramCost', 'Percent', 'CreatedUserId', 'CreatedDate', 'DiscoverLeakDate', 'DiscoverLeakDateShamsi', 'EndDate', 'ConfirmUserId', 'ActionId', 'RequestId2', 'RequestProvinceId', 'RequestActivityId', 'RequestCycleId', 'RequestCustomerEffectId', 'RequestOrganizationEffectId', 'RequestRiskLevelId', 'RequestIncomeFlowId', 'RequestCount', 'RequestCycleCost', 'RequestFactor', 'RequestDelayedCost', 'RequestYearCost', 'RequestAccessibleCost', 'RequestInaccessibleCost', 'RequestFinancial', 'RequestLeakDate', 'RequestDiscoverLeakDate', 'RequestDiscoverLeakDateShamsi', 'RequestEventDescription', 'RequestMainReason', 'RequestCorrectionOperation', 'RequestAvoidRepeatingOperation', 'RequestCreatedUserId', 'RequestCreatedDate', 'RequestCreatedDateShamsi', 'RequestModifiedUserId', 'RequestModifiedDate', 'RequestIsDeleted', 'RequestDeletedUserId', 'RequestDeletedDate', 'RequestEndDate', 'RequestStatusId', 'RequestActionId', 'ProvinceName', 'ProvinceEnglishName', 'ProvinceManagerName', 'ProvinceLetterNo', 'ProvincePmoLevelId', 'ProvinceInstallDate', 'ProvinceCreatedUserId', 'ProvinceCreatedDate', 'ProvinceModifiedUserId', 'ProvinceModifiedDate', 'ProvinceIsDeleted', 'ProvinceDeletedUserId', 'ProvinceDeletedDate', 'ActivityCode', 'ActivityName', 'ActivityEnglishName', 'ActivityObjective', 'ActivityEnglishObjective', 'ActivityCreatedUserId', 'ActivityCreatedDate', 'ActivityModifiedUserId', 'ActivityModifiedDate', 'ActivityIsDeleted', 'ActivityDeletedUserId', 'ActivityDeletedDate', 'ActivityGroupId', 'ActivityRepeatTermId', 'ActivityKeyCheckArea', 'ActivityDataSource', 'ActivityMethodology', 'ActivityKeyFocus', 'ActivityAction', 'ActivityKpi'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityReportRow = StimulReport.ActivityReportRow || (StimulReport.ActivityReportRow = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityReportService;
        (function (ActivityReportService) {
            ActivityReportService.baseUrl = 'StimulReport/ActivityReport';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityReportService.Methods || (ActivityReportService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityReportService[x] = function (r, s, o) { return Q.serviceRequest(ActivityReportService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityReportService.baseUrl + '/' + x;
            });
        })(ActivityReportService = StimulReport.ActivityReportService || (StimulReport.ActivityReportService = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestDetailForm = (function (_super) {
            __extends(ActivityRequestDetailForm, _super);
            function ActivityRequestDetailForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestDetailForm.formKey = 'StimulReport.ActivityRequestDetail';
            return ActivityRequestDetailForm;
        }(Serenity.PrefixedContext));
        StimulReport.ActivityRequestDetailForm = ActivityRequestDetailForm;
        [['Id2', function () { return Serenity.IntegerEditor; }], ['ProvinceId', function () { return Serenity.LookupEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['CustomerEffectId', function () { return Serenity.LookupEditor; }], ['RiskLevelId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.StringEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.StringEditor; }], ['YearCost', function () { return Serenity.StringEditor; }], ['AccessibleCost', function () { return Serenity.StringEditor; }], ['InaccessibleCost', function () { return Serenity.StringEditor; }], ['TotalLeakage', function () { return Serenity.StringEditor; }], ['RecoverableLeakage', function () { return Serenity.StringEditor; }], ['Recovered', function () { return Serenity.StringEditor; }], ['Financial', function () { return Serenity.StringEditor; }], ['LeakDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDateShamsi', function () { return Serenity.StringEditor; }], ['EventDescription', function () { return Serenity.StringEditor; }], ['MainReason', function () { return Serenity.StringEditor; }], ['CorrectionOperation', function () { return Serenity.StringEditor; }], ['AvoidRepeatingOperation', function () { return Serenity.StringEditor; }], ['CreatedUserId', function () { return Serenity.LookupEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['CreatedDateShamsi', function () { return Serenity.StringEditor; }], ['ModifiedUserId', function () { return Serenity.LookupEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.LookupEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }], ['EndDate', function () { return Serenity.DateEditor; }], ['StatusId', function () { return Serenity.LookupEditor; }], ['ActionId', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestDetailForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestDetailRow;
        (function (ActivityRequestDetailRow) {
            ActivityRequestDetailRow.idProperty = 'Id';
            ActivityRequestDetailRow.localTextPrefix = 'StimulReport.ActivityRequestDetail';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestDetailRow.Fields || (ActivityRequestDetailRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestDetailRow = StimulReport.ActivityRequestDetailRow || (StimulReport.ActivityRequestDetailRow = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestDetailService;
        (function (ActivityRequestDetailService) {
            ActivityRequestDetailService.baseUrl = 'StimulReport/ActivityRequestDetail';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestDetailService.Methods || (ActivityRequestDetailService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestDetailService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestDetailService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestDetailService.baseUrl + '/' + x;
            });
        })(ActivityRequestDetailService = StimulReport.ActivityRequestDetailService || (StimulReport.ActivityRequestDetailService = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestReportForm = (function (_super) {
            __extends(ActivityRequestReportForm, _super);
            function ActivityRequestReportForm() {
                _super.apply(this, arguments);
            }
            ActivityRequestReportForm.formKey = 'StimulReport.ActivityRequestReport';
            return ActivityRequestReportForm;
        }(Serenity.PrefixedContext));
        StimulReport.ActivityRequestReportForm = ActivityRequestReportForm;
        [['Id2', function () { return Serenity.IntegerEditor; }], ['ProvinceId', function () { return Serenity.LookupEditor; }], ['ActivityId', function () { return Serenity.LookupEditor; }], ['ActivityCode', function () { return Serenity.StringEditor; }], ['CycleId', function () { return Serenity.LookupEditor; }], ['CustomerEffectId', function () { return Serenity.LookupEditor; }], ['RiskLevelId', function () { return Serenity.LookupEditor; }], ['IncomeFlowId', function () { return Serenity.LookupEditor; }], ['Count', function () { return Serenity.IntegerEditor; }], ['CycleCost', function () { return Serenity.StringEditor; }], ['Factor', function () { return Serenity.StringEditor; }], ['DelayedCost', function () { return Serenity.StringEditor; }], ['YearCost', function () { return Serenity.StringEditor; }], ['AccessibleCost', function () { return Serenity.StringEditor; }], ['InaccessibleCost', function () { return Serenity.StringEditor; }], ['TotalLeakage', function () { return Serenity.StringEditor; }], ['RecoverableLeakage', function () { return Serenity.StringEditor; }], ['Recovered', function () { return Serenity.StringEditor; }], ['Financial', function () { return Serenity.StringEditor; }], ['LeakDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDate', function () { return Serenity.DateEditor; }], ['DiscoverLeakDateShamsi', function () { return Serenity.StringEditor; }], ['EventDescription', function () { return Serenity.StringEditor; }], ['MainReason', function () { return Serenity.StringEditor; }], ['CorrectionOperation', function () { return Serenity.StringEditor; }], ['AvoidRepeatingOperation', function () { return Serenity.StringEditor; }], ['CreatedUserId', function () { return Serenity.LookupEditor; }], ['CreatedDate', function () { return Serenity.DateEditor; }], ['CreatedDateShamsi', function () { return Serenity.StringEditor; }], ['ModifiedUserId', function () { return Serenity.LookupEditor; }], ['ModifiedDate', function () { return Serenity.DateEditor; }], ['SendDate', function () { return Serenity.DateEditor; }], ['SendUserId', function () { return Serenity.LookupEditor; }], ['IsDeleted', function () { return Serenity.BooleanEditor; }], ['DeletedUserId', function () { return Serenity.LookupEditor; }], ['DeletedDate', function () { return Serenity.DateEditor; }], ['EndDate', function () { return Serenity.DateEditor; }], ['StatusId', function () { return Serenity.LookupEditor; }], ['ActionId', function () { return Serenity.IntegerEditor; }], ['File1', function () { return Serenity.StringEditor; }], ['File2', function () { return Serenity.StringEditor; }], ['File3', function () { return Serenity.StringEditor; }], ['ConfirmTypeId', function () { return Serenity.EnumEditor; }], ['IsRejected', function () { return Serenity.BooleanEditor; }], ['FinancialControllerConfirm', function () { return Serenity.BooleanEditor; }]].forEach(function (x) { return Object.defineProperty(ActivityRequestReportForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestReportRow;
        (function (ActivityRequestReportRow) {
            ActivityRequestReportRow.idProperty = 'Id';
            ActivityRequestReportRow.localTextPrefix = 'StimulReport.ActivityRequestReport';
            var Fields;
            (function (Fields) {
            })(Fields = ActivityRequestReportRow.Fields || (ActivityRequestReportRow.Fields = {}));
            ['Id', 'Count', 'CycleCost', 'Factor', 'DelayedCost', 'YearCost', 'AccessibleCost', 'InaccessibleCost', 'Financial', 'TotalLeakage', 'RecoverableLeakage', 'Recovered', 'EventDescription', 'MainReason', 'CorrectionOperation', 'AvoidRepeatingOperation', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'SendDate', 'SendUserId', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'EndDate', 'ActivityId', 'ProvinceId', 'CycleId', 'CustomerEffectId', 'IncomeFlowId', 'RiskLevelId', 'StatusID', 'LeakDate', 'DiscoverLeakDate', 'ActivityCode', 'ActivityName', 'ActivityObjective', 'ActivityGroupId', 'ProvinceName', 'CycleName', 'CustomerEffectName', 'IncomeFlowName', 'RiskLevelName', 'StatusName', 'CreatedUserName', 'ModifiedUserName', 'DeletedUserName', 'SendUserName', 'ConfirmTypeID', 'IsRejected', 'FinancialControllerConfirm'].forEach(function (x) { return Fields[x] = x; });
        })(ActivityRequestReportRow = StimulReport.ActivityRequestReportRow || (StimulReport.ActivityRequestReportRow = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestReportService;
        (function (ActivityRequestReportService) {
            ActivityRequestReportService.baseUrl = 'StimulReport/ActivityRequestReport';
            var Methods;
            (function (Methods) {
            })(Methods = ActivityRequestReportService.Methods || (ActivityRequestReportService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                ActivityRequestReportService[x] = function (r, s, o) { return Q.serviceRequest(ActivityRequestReportService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = ActivityRequestReportService.baseUrl + '/' + x;
            });
        })(ActivityRequestReportService = StimulReport.ActivityRequestReportService || (StimulReport.ActivityRequestReportService = {}));
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowActionForm = (function (_super) {
            __extends(WorkFlowActionForm, _super);
            function WorkFlowActionForm() {
                _super.apply(this, arguments);
            }
            WorkFlowActionForm.formKey = 'WorkFlow.WorkFlowAction';
            return WorkFlowActionForm;
        }(Serenity.PrefixedContext));
        WorkFlow.WorkFlowActionForm = WorkFlowActionForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(WorkFlowActionForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowActionRow;
        (function (WorkFlowActionRow) {
            WorkFlowActionRow.idProperty = 'Id';
            WorkFlowActionRow.nameProperty = 'Name';
            WorkFlowActionRow.localTextPrefix = 'WorkFlow.WorkFlowAction';
            WorkFlowActionRow.lookupKey = 'WorkFlow.WorkFlowAction';
            function getLookup() {
                return Q.getLookup('WorkFlow.WorkFlowAction');
            }
            WorkFlowActionRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = WorkFlowActionRow.Fields || (WorkFlowActionRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(WorkFlowActionRow = WorkFlow.WorkFlowActionRow || (WorkFlow.WorkFlowActionRow = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowActionService;
        (function (WorkFlowActionService) {
            WorkFlowActionService.baseUrl = 'WorkFlow/WorkFlowAction';
            var Methods;
            (function (Methods) {
            })(Methods = WorkFlowActionService.Methods || (WorkFlowActionService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                WorkFlowActionService[x] = function (r, s, o) { return Q.serviceRequest(WorkFlowActionService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = WorkFlowActionService.baseUrl + '/' + x;
            });
        })(WorkFlowActionService = WorkFlow.WorkFlowActionService || (WorkFlow.WorkFlowActionService = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowRuleForm = (function (_super) {
            __extends(WorkFlowRuleForm, _super);
            function WorkFlowRuleForm() {
                _super.apply(this, arguments);
            }
            WorkFlowRuleForm.formKey = 'WorkFlow.WorkFlowRule';
            return WorkFlowRuleForm;
        }(Serenity.PrefixedContext));
        WorkFlow.WorkFlowRuleForm = WorkFlowRuleForm;
        [['CurrentStatusId', function () { return Serenity.LookupEditor; }], ['ActionId', function () { return Serenity.LookupEditor; }], ['NextStatusId', function () { return Serenity.LookupEditor; }], ['Version', function () { return Serenity.IntegerEditor; }]].forEach(function (x) { return Object.defineProperty(WorkFlowRuleForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowRuleRow;
        (function (WorkFlowRuleRow) {
            WorkFlowRuleRow.idProperty = 'Id';
            WorkFlowRuleRow.localTextPrefix = 'WorkFlow.WorkFlowRule';
            var Fields;
            (function (Fields) {
            })(Fields = WorkFlowRuleRow.Fields || (WorkFlowRuleRow.Fields = {}));
            ['Id', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'ActionId', 'CurrentStatusId', 'NextStatusId', 'Version', 'ActionName', 'CurrentStatusName', 'NextStatusName'].forEach(function (x) { return Fields[x] = x; });
        })(WorkFlowRuleRow = WorkFlow.WorkFlowRuleRow || (WorkFlow.WorkFlowRuleRow = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowRuleService;
        (function (WorkFlowRuleService) {
            WorkFlowRuleService.baseUrl = 'WorkFlow/WorkFlowRule';
            var Methods;
            (function (Methods) {
            })(Methods = WorkFlowRuleService.Methods || (WorkFlowRuleService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                WorkFlowRuleService[x] = function (r, s, o) { return Q.serviceRequest(WorkFlowRuleService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = WorkFlowRuleService.baseUrl + '/' + x;
            });
        })(WorkFlowRuleService = WorkFlow.WorkFlowRuleService || (WorkFlow.WorkFlowRuleService = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusForm = (function (_super) {
            __extends(WorkFlowStatusForm, _super);
            function WorkFlowStatusForm() {
                _super.apply(this, arguments);
            }
            WorkFlowStatusForm.formKey = 'WorkFlow.WorkFlowStatus';
            return WorkFlowStatusForm;
        }(Serenity.PrefixedContext));
        WorkFlow.WorkFlowStatusForm = WorkFlowStatusForm;
        [['StepId', function () { return Serenity.LookupEditor; }], ['StatusTypeId', function () { return Serenity.LookupEditor; }]].forEach(function (x) { return Object.defineProperty(WorkFlowStatusForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusRow;
        (function (WorkFlowStatusRow) {
            WorkFlowStatusRow.idProperty = 'Id';
            WorkFlowStatusRow.nameProperty = 'FullName';
            WorkFlowStatusRow.localTextPrefix = 'WorkFlow.WorkFlowStatus';
            WorkFlowStatusRow.lookupKey = 'WorkFlow.WorkFlowStatus';
            function getLookup() {
                return Q.getLookup('WorkFlow.WorkFlowStatus');
            }
            WorkFlowStatusRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = WorkFlowStatusRow.Fields || (WorkFlowStatusRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate', 'StepId', 'StatusTypeId', 'StepName', 'StatusTypeName', 'FullName'].forEach(function (x) { return Fields[x] = x; });
        })(WorkFlowStatusRow = WorkFlow.WorkFlowStatusRow || (WorkFlow.WorkFlowStatusRow = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusService;
        (function (WorkFlowStatusService) {
            WorkFlowStatusService.baseUrl = 'WorkFlow/WorkFlowStatus';
            var Methods;
            (function (Methods) {
            })(Methods = WorkFlowStatusService.Methods || (WorkFlowStatusService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                WorkFlowStatusService[x] = function (r, s, o) { return Q.serviceRequest(WorkFlowStatusService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = WorkFlowStatusService.baseUrl + '/' + x;
            });
        })(WorkFlowStatusService = WorkFlow.WorkFlowStatusService || (WorkFlow.WorkFlowStatusService = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusTypeForm = (function (_super) {
            __extends(WorkFlowStatusTypeForm, _super);
            function WorkFlowStatusTypeForm() {
                _super.apply(this, arguments);
            }
            WorkFlowStatusTypeForm.formKey = 'WorkFlow.WorkFlowStatusType';
            return WorkFlowStatusTypeForm;
        }(Serenity.PrefixedContext));
        WorkFlow.WorkFlowStatusTypeForm = WorkFlowStatusTypeForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(WorkFlowStatusTypeForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusTypeRow;
        (function (WorkFlowStatusTypeRow) {
            WorkFlowStatusTypeRow.idProperty = 'Id';
            WorkFlowStatusTypeRow.nameProperty = 'Name';
            WorkFlowStatusTypeRow.localTextPrefix = 'WorkFlow.WorkFlowStatusType';
            WorkFlowStatusTypeRow.lookupKey = 'WorkFlow.WorkFlowStatusType';
            function getLookup() {
                return Q.getLookup('WorkFlow.WorkFlowStatusType');
            }
            WorkFlowStatusTypeRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = WorkFlowStatusTypeRow.Fields || (WorkFlowStatusTypeRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(WorkFlowStatusTypeRow = WorkFlow.WorkFlowStatusTypeRow || (WorkFlow.WorkFlowStatusTypeRow = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusTypeService;
        (function (WorkFlowStatusTypeService) {
            WorkFlowStatusTypeService.baseUrl = 'WorkFlow/WorkFlowStatusType';
            var Methods;
            (function (Methods) {
            })(Methods = WorkFlowStatusTypeService.Methods || (WorkFlowStatusTypeService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                WorkFlowStatusTypeService[x] = function (r, s, o) { return Q.serviceRequest(WorkFlowStatusTypeService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = WorkFlowStatusTypeService.baseUrl + '/' + x;
            });
        })(WorkFlowStatusTypeService = WorkFlow.WorkFlowStatusTypeService || (WorkFlow.WorkFlowStatusTypeService = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStepForm = (function (_super) {
            __extends(WorkFlowStepForm, _super);
            function WorkFlowStepForm() {
                _super.apply(this, arguments);
            }
            WorkFlowStepForm.formKey = 'WorkFlow.WorkFlowStep';
            return WorkFlowStepForm;
        }(Serenity.PrefixedContext));
        WorkFlow.WorkFlowStepForm = WorkFlowStepForm;
        [['Name', function () { return Serenity.StringEditor; }]].forEach(function (x) { return Object.defineProperty(WorkFlowStepForm.prototype, x[0], { get: function () { return this.w(x[0], x[1]()); }, enumerable: true, configurable: true }); });
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStepRow;
        (function (WorkFlowStepRow) {
            WorkFlowStepRow.idProperty = 'Id';
            WorkFlowStepRow.nameProperty = 'Name';
            WorkFlowStepRow.localTextPrefix = 'WorkFlow.WorkFlowStep';
            WorkFlowStepRow.lookupKey = 'WorkFlow.WorkFlowStep';
            function getLookup() {
                return Q.getLookup('WorkFlow.WorkFlowStep');
            }
            WorkFlowStepRow.getLookup = getLookup;
            var Fields;
            (function (Fields) {
            })(Fields = WorkFlowStepRow.Fields || (WorkFlowStepRow.Fields = {}));
            ['Id', 'Name', 'CreatedUserId', 'CreatedDate', 'ModifiedUserId', 'ModifiedDate', 'IsDeleted', 'DeletedUserId', 'DeletedDate'].forEach(function (x) { return Fields[x] = x; });
        })(WorkFlowStepRow = WorkFlow.WorkFlowStepRow || (WorkFlow.WorkFlowStepRow = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStepService;
        (function (WorkFlowStepService) {
            WorkFlowStepService.baseUrl = 'WorkFlow/WorkFlowStep';
            var Methods;
            (function (Methods) {
            })(Methods = WorkFlowStepService.Methods || (WorkFlowStepService.Methods = {}));
            ['Create', 'Update', 'Delete', 'Retrieve', 'List'].forEach(function (x) {
                WorkFlowStepService[x] = function (r, s, o) { return Q.serviceRequest(WorkFlowStepService.baseUrl + '/' + x, r, s, o); };
                Methods[x] = WorkFlowStepService.baseUrl + '/' + x;
            });
        })(WorkFlowStepService = WorkFlow.WorkFlowStepService || (WorkFlow.WorkFlowStepService = {}));
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var LanguageSelection = (function (_super) {
            __extends(LanguageSelection, _super);
            function LanguageSelection(select, currentLanguage) {
                _super.call(this, select);
                currentLanguage = Q.coalesce(currentLanguage, 'en');
                this.change(function (e) {
                    $.cookie('LanguagePreference', select.val(), {
                        path: Q.Config.applicationPath,
                        expires: 365
                    });
                    window.location.reload(true);
                });
                Q.getLookupAsync('Administration.Language').then(function (x) {
                    if (!Q.any(x.items, function (z) { return z.LanguageId === currentLanguage; })) {
                        var idx = currentLanguage.lastIndexOf('-');
                        if (idx >= 0) {
                            currentLanguage = currentLanguage.substr(0, idx);
                            if (!Q.any(x.items, function (y) { return y.LanguageId === currentLanguage; })) {
                                currentLanguage = 'en';
                            }
                        }
                        else {
                            currentLanguage = 'en';
                        }
                    }
                    for (var _i = 0, _a = x.items; _i < _a.length; _i++) {
                        var l = _a[_i];
                        Q.addOption(select, l.LanguageId, l.LanguageName);
                    }
                    select.val(currentLanguage);
                });
            }
            return LanguageSelection;
        }(Serenity.Widget));
        Common.LanguageSelection = LanguageSelection;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var SidebarSearch = (function (_super) {
            __extends(SidebarSearch, _super);
            function SidebarSearch(input, menuUL) {
                var _this = this;
                _super.call(this, input);
                new Serenity.QuickSearchInput(input, {
                    onSearch: function (field, text, success) {
                        _this.updateMatchFlags(text);
                        success(true);
                    }
                });
                this.menuUL = menuUL;
            }
            SidebarSearch.prototype.updateMatchFlags = function (text) {
                var liList = this.menuUL.find('li').removeClass('non-match');
                text = Q.trimToNull(text);
                if (text == null) {
                    liList.show();
                    liList.removeClass('expanded');
                    return;
                }
                var parts = text.replace(',', ' ').split(' ').filter(function (x) { return !Q.isTrimmedEmpty(x); });
                for (var i = 0; i < parts.length; i++) {
                    parts[i] = Q.trimToNull(Select2.util.stripDiacritics(parts[i]).toUpperCase());
                }
                var items = liList;
                items.each(function (idx, e) {
                    var x = $(e);
                    var title = Select2.util.stripDiacritics(Q.coalesce(x.text(), '').toUpperCase());
                    for (var _i = 0, parts_1 = parts; _i < parts_1.length; _i++) {
                        var p = parts_1[_i];
                        if (p != null && !(title.indexOf(p) !== -1)) {
                            x.addClass('non-match');
                            break;
                        }
                    }
                });
                var matchingItems = items.not('.non-match');
                var visibles = matchingItems.parents('li').add(matchingItems);
                var nonVisibles = liList.not(visibles);
                nonVisibles.hide().addClass('non-match');
                visibles.show();
                liList.addClass('expanded');
            };
            return SidebarSearch;
        }(Serenity.Widget));
        Common.SidebarSearch = SidebarSearch;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var ThemeSelection = (function (_super) {
            __extends(ThemeSelection, _super);
            function ThemeSelection(select) {
                var _this = this;
                _super.call(this, select);
                this.change(function (e) {
                    $.cookie('ThemePreference', select.val(), {
                        path: Q.Config.applicationPath,
                        expires: 365
                    });
                    $('body').removeClass('skin-' + _this.getCurrentTheme());
                    $('body').addClass('skin-' + select.val());
                });
                Q.addOption(select, 'blue', Q.text('Site.Layout.ThemeBlue'));
                Q.addOption(select, 'blue-light', Q.text('Site.Layout.ThemeBlueLight'));
                Q.addOption(select, 'purple', Q.text('Site.Layout.ThemePurple'));
                Q.addOption(select, 'purple-light', Q.text('Site.Layout.ThemePurpleLight'));
                Q.addOption(select, 'red', Q.text('Site.Layout.ThemeRed'));
                Q.addOption(select, 'red-light', Q.text('Site.Layout.ThemeRedLight'));
                Q.addOption(select, 'green', Q.text('Site.Layout.ThemeGreen'));
                Q.addOption(select, 'green-light', Q.text('Site.Layout.ThemeGreenLight'));
                Q.addOption(select, 'yellow', Q.text('Site.Layout.ThemeYellow'));
                Q.addOption(select, 'yellow-light', Q.text('Site.Layout.ThemeYellowLight'));
                Q.addOption(select, 'black', Q.text('Site.Layout.ThemeBlack'));
                Q.addOption(select, 'black-light', Q.text('Site.Layout.ThemeBlackLight'));
                select.val(this.getCurrentTheme());
            }
            ThemeSelection.prototype.getCurrentTheme = function () {
                var skinClass = Q.first(($('body').attr('class') || '').split(' '), function (x) { return Q.startsWith(x, 'skin-'); });
                if (skinClass) {
                    return skinClass.substr(5);
                }
                return 'blue';
            };
            return ThemeSelection;
        }(Serenity.Widget));
        Common.ThemeSelection = ThemeSelection;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var PdfExportHelper;
        (function (PdfExportHelper) {
            function toAutoTableColumns(srcColumns, columnStyles, columnTitles) {
                return srcColumns.map(function (src) {
                    var col = {
                        dataKey: src.id || src.field,
                        title: src.name || ''
                    };
                    if (columnTitles && columnTitles[col.dataKey] != null)
                        col.title = columnTitles[col.dataKey];
                    var style = {};
                    if ((src.cssClass || '').indexOf("align-right") >= 0)
                        style.halign = 'right';
                    else if ((src.cssClass || '').indexOf("align-center") >= 0)
                        style.halign = 'center';
                    columnStyles[col.dataKey] = style;
                    return col;
                });
            }
            function toAutoTableData(entities, keys, srcColumns) {
                var el = document.createElement('span');
                var row = 0;
                return entities.map(function (item) {
                    var dst = {};
                    for (var cell = 0; cell < srcColumns.length; cell++) {
                        var src = srcColumns[cell];
                        var fld = src.field || '';
                        var key = keys[cell];
                        var txt = void 0;
                        var html = void 0;
                        if (src.formatter) {
                            html = src.formatter(row, cell, item[fld], src, item);
                        }
                        else if (src.format) {
                            html = src.format({ row: row, cell: cell, item: item, value: item[fld] });
                        }
                        else {
                            dst[key] = item[fld];
                            continue;
                        }
                        if (!html || (html.indexOf('<') < 0 && html.indexOf('&') < 0))
                            dst[key] = html;
                        else {
                            el.innerHTML = html;
                            if (el.children.length == 1 &&
                                $(el.children[0]).is(":input")) {
                                dst[key] = $(el.children[0]).val();
                            }
                            else if (el.children.length == 1 &&
                                $(el.children).is('.check-box')) {
                                dst[key] = $(el.children).hasClass("checked") ? "X" : "";
                            }
                            else
                                dst[key] = el.textContent || '';
                        }
                    }
                    row++;
                    return dst;
                });
            }
            function exportToPdf(options) {
                var g = options.grid;
                if (!options.onViewSubmit())
                    return;
                includeAutoTable();
                var request = Q.deepClone(g.view.params);
                request.Take = 0;
                request.Skip = 0;
                var sortBy = g.view.sortBy;
                if (sortBy != null)
                    request.Sort = sortBy;
                var gridColumns = g.slickGrid.getColumns();
                gridColumns = gridColumns.filter(function (x) { return x.id !== "__select__"; });
                request.IncludeColumns = [];
                for (var _i = 0, gridColumns_1 = gridColumns; _i < gridColumns_1.length; _i++) {
                    var column = gridColumns_1[_i];
                    request.IncludeColumns.push(column.id || column.field);
                }
                Q.serviceCall({
                    url: g.view.url,
                    request: request,
                    onSuccess: function (response) {
                        var doc = new jsPDF('l', 'pt');
                        var srcColumns = gridColumns;
                        var columnStyles = {};
                        var columns = toAutoTableColumns(srcColumns, columnStyles, options.columnTitles);
                        var keys = columns.map(function (x) { return x.dataKey; });
                        var entities = response.Entities || [];
                        var data = toAutoTableData(entities, keys, srcColumns);
                        doc.setFontSize(options.titleFontSize || 10);
                        doc.setFontStyle('bold');
                        var reportTitle = options.reportTitle || g.getTitle() || "Report";
                        doc.autoTableText(reportTitle, doc.internal.pageSize.width / 2, options.titleTop || 25, { halign: 'center' });
                        var totalPagesExp = "{{T}}";
                        var pageNumbers = options.pageNumbers == null || options.pageNumbers;
                        var autoOptions = $.extend({
                            margin: { top: 25, left: 25, right: 25, bottom: pageNumbers ? 25 : 30 },
                            startY: 60,
                            styles: {
                                fontSize: 8,
                                overflow: 'linebreak',
                                cellPadding: 2,
                                valign: 'middle'
                            },
                            columnStyles: columnStyles
                        }, options.tableOptions);
                        if (pageNumbers) {
                            var footer = function (data) {
                                var str = data.pageCount;
                                // Total page number plugin only available in jspdf v1.0+
                                if (typeof doc.putTotalPages === 'function') {
                                    str = str + " / " + totalPagesExp;
                                }
                                doc.autoTableText(str, doc.internal.pageSize.width / 2, doc.internal.pageSize.height - autoOptions.margin.bottom, {
                                    halign: 'center'
                                });
                            };
                            autoOptions.afterPageContent = footer;
                        }
                        doc.autoTable(columns, data, autoOptions);
                        if (typeof doc.putTotalPages === 'function') {
                            doc.putTotalPages(totalPagesExp);
                        }
                        if (!options.output || options.output == "file") {
                            var fileName = options.reportTitle || "{0}_{1}.pdf";
                            fileName = Q.format(fileName, g.getTitle() || "report", Q.formatDate(new Date(), "yyyyMMdd_HHmm"));
                            doc.save(fileName);
                            return;
                        }
                        if (options.autoPrint)
                            doc.autoPrint();
                        var output = options.output;
                        if (output == 'newwindow' || '_blank')
                            output = 'dataurlnewwindow';
                        else if (output == 'window')
                            output = 'datauri';
                        doc.output(output);
                    }
                });
            }
            PdfExportHelper.exportToPdf = exportToPdf;
            function createToolButton(options) {
                return {
                    title: options.title || '',
                    hint: options.hint || 'PDF',
                    cssClass: 'export-pdf-button',
                    onClick: function () { return exportToPdf(options); },
                    separator: options.separator
                };
            }
            PdfExportHelper.createToolButton = createToolButton;
            function includeJsPDF() {
                if (typeof jsPDF !== "undefined")
                    return;
                var script = $("jsPDFScript");
                if (script.length > 0)
                    return;
                $("<script/>")
                    .attr("type", "text/javascript")
                    .attr("id", "jsPDFScript")
                    .attr("src", Q.resolveUrl("~/Scripts/jspdf.min.js"))
                    .appendTo(document.head);
            }
            function includeAutoTable() {
                includeJsPDF();
                if (typeof jsPDF === "undefined" ||
                    typeof jsPDF.API == "undefined" ||
                    typeof jsPDF.API.autoTable !== "undefined")
                    return;
                var script = $("jsPDFAutoTableScript");
                if (script.length > 0)
                    return;
                $("<script/>")
                    .attr("type", "text/javascript")
                    .attr("id", "jsPDFAutoTableScript")
                    .attr("src", Q.resolveUrl("~/Scripts/jspdf.plugin.autotable.min.js"))
                    .appendTo(document.head);
            }
        })(PdfExportHelper = Common.PdfExportHelper || (Common.PdfExportHelper = {}));
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var UserPreferenceStorage = (function () {
            function UserPreferenceStorage() {
            }
            UserPreferenceStorage.prototype.getItem = function (key) {
                var value;
                Common.UserPreferenceService.Retrieve({
                    PreferenceType: "UserPreferenceStorage",
                    Name: key
                }, function (response) { return value = response.Value; }, {
                    async: false
                });
                return value;
            };
            UserPreferenceStorage.prototype.setItem = function (key, data) {
                Common.UserPreferenceService.Update({
                    PreferenceType: "UserPreferenceStorage",
                    Name: key,
                    Value: data
                });
            };
            return UserPreferenceStorage;
        }());
        Common.UserPreferenceStorage = UserPreferenceStorage;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var LoginPanel = (function (_super) {
            __extends(LoginPanel, _super);
            function LoginPanel(container) {
                var _this = this;
                _super.call(this, container);
                $(function () {
                    $('body').vegas({
                        delay: 10000,
                        cover: true,
                        overlay: Q.resolveUrl("~/scripts/vegas/overlays/01.png"),
                        slides: []
                    });
                });
                this.form = new Membership.LoginForm(this.idPrefix);
                this.byId('LoginButton').click(function (e) {
                    e.preventDefault();
                    if (!_this.validateForm()) {
                        return;
                    }
                    var request = _this.getSaveEntity();
                    Q.serviceCall({
                        url: Q.resolveUrl('~/Account/Login'),
                        request: request,
                        onSuccess: function (response) {
                            var q = Q.parseQueryString();
                            var returnUrl = q['returnUrl'] || q['ReturnUrl'];
                            if (returnUrl) {
                                window.location.href = returnUrl;
                            }
                            else {
                                window.location.href = Q.resolveUrl('~/');
                            }
                        }
                    });
                });
            }
            LoginPanel.prototype.getFormKey = function () { return Membership.LoginForm.formKey; };
            LoginPanel = __decorate([
                Serenity.Decorators.registerClass()
            ], LoginPanel);
            return LoginPanel;
        }(Serenity.PropertyPanel));
        Membership.LoginPanel = LoginPanel;
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ChangePasswordPanel = (function (_super) {
            __extends(ChangePasswordPanel, _super);
            function ChangePasswordPanel(container) {
                var _this = this;
                _super.call(this, container);
                this.form = new Membership.ChangePasswordForm(this.idPrefix);
                this.form.NewPassword.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.w('ConfirmPassword', Serenity.PasswordEditor).value.length < 4) {
                        return Q.format(Q.text('Validation.MinRequiredPasswordLength'), 4);
                    }
                });
                this.form.ConfirmPassword.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.ConfirmPassword.value !== _this.form.NewPassword.value) {
                        return Q.text('Validation.PasswordConfirm');
                    }
                });
                this.byId('SubmitButton').click(function (e) {
                    e.preventDefault();
                    if (!_this.validateForm()) {
                        return;
                    }
                    var request = _this.getSaveEntity();
                    Q.serviceCall({
                        url: Q.resolveUrl('~/Account/ChangePassword'),
                        request: request,
                        onSuccess: function (response) {
                            Q.information(Q.text('Forms.Membership.ChangePassword.Success'), function () {
                                window.location.href = Q.resolveUrl('~/');
                            });
                        }
                    });
                });
            }
            ChangePasswordPanel.prototype.getFormKey = function () { return Membership.ChangePasswordForm.formKey; };
            ChangePasswordPanel = __decorate([
                Serenity.Decorators.registerClass()
            ], ChangePasswordPanel);
            return ChangePasswordPanel;
        }(Serenity.PropertyPanel));
        Membership.ChangePasswordPanel = ChangePasswordPanel;
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ForgotPasswordPanel = (function (_super) {
            __extends(ForgotPasswordPanel, _super);
            function ForgotPasswordPanel(container) {
                var _this = this;
                _super.call(this, container);
                this.form = new Membership.ForgotPasswordForm(this.idPrefix);
                this.byId('SubmitButton').click(function (e) {
                    e.preventDefault();
                    if (!_this.validateForm()) {
                        return;
                    }
                    var request = _this.getSaveEntity();
                    Q.serviceCall({
                        url: Q.resolveUrl('~/Account/ForgotPassword'),
                        request: request,
                        onSuccess: function (response) {
                            Q.information(Q.text('Forms.Membership.ForgotPassword.Success'), function () {
                                window.location.href = Q.resolveUrl('~/');
                            });
                        }
                    });
                });
            }
            ForgotPasswordPanel.prototype.getFormKey = function () { return Membership.ForgotPasswordForm.formKey; };
            ForgotPasswordPanel = __decorate([
                Serenity.Decorators.registerClass()
            ], ForgotPasswordPanel);
            return ForgotPasswordPanel;
        }(Serenity.PropertyPanel));
        Membership.ForgotPasswordPanel = ForgotPasswordPanel;
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var ResetPasswordPanel = (function (_super) {
            __extends(ResetPasswordPanel, _super);
            function ResetPasswordPanel(container) {
                var _this = this;
                _super.call(this, container);
                this.form = new Membership.ResetPasswordForm(this.idPrefix);
                this.form.NewPassword.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.ConfirmPassword.value.length < 7) {
                        return Q.format(Q.text('Validation.MinRequiredPasswordLength'), 4);
                    }
                });
                this.form.ConfirmPassword.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.ConfirmPassword.value !== _this.form.NewPassword.value) {
                        return Q.text('Validation.PasswordConfirm');
                    }
                });
                this.byId('SubmitButton').click(function (e) {
                    e.preventDefault();
                    if (!_this.validateForm()) {
                        return;
                    }
                    var request = _this.getSaveEntity();
                    request.Token = _this.byId('Token').val();
                    Q.serviceCall({
                        url: Q.resolveUrl('~/Account/ResetPassword'),
                        request: request,
                        onSuccess: function (response) {
                            Q.information(Q.text('Forms.Membership.ResetPassword.Success'), function () {
                                window.location.href = Q.resolveUrl('~/Account/Login');
                            });
                        }
                    });
                });
            }
            ResetPasswordPanel.prototype.getFormKey = function () { return Membership.ResetPasswordForm.formKey; };
            ResetPasswordPanel = __decorate([
                Serenity.Decorators.registerClass()
            ], ResetPasswordPanel);
            return ResetPasswordPanel;
        }(Serenity.PropertyPanel));
        Membership.ResetPasswordPanel = ResetPasswordPanel;
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Membership;
    (function (Membership) {
        var SignUpPanel = (function (_super) {
            __extends(SignUpPanel, _super);
            function SignUpPanel(container) {
                var _this = this;
                _super.call(this, container);
                this.form = new Membership.SignUpForm(this.idPrefix);
                this.form.ConfirmEmail.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.ConfirmEmail.value !== _this.form.Email.value) {
                        return Q.text('Validation.EmailConfirm');
                    }
                });
                this.form.ConfirmPassword.addValidationRule(this.uniqueName, function (e) {
                    if (_this.form.ConfirmPassword.value !== _this.form.Password.value) {
                        return Q.text('Validation.PasswordConfirm');
                    }
                });
                this.byId('SubmitButton').click(function (e) {
                    e.preventDefault();
                    if (!_this.validateForm()) {
                        return;
                    }
                    Q.serviceCall({
                        url: Q.resolveUrl('~/Account/SignUp'),
                        request: {
                            DisplayName: _this.form.DisplayName.value,
                            Email: _this.form.Email.value,
                            Password: _this.form.Password.value
                        },
                        onSuccess: function (response) {
                            Q.information(Q.text('Forms.Membership.SignUp.Success'), function () {
                                window.location.href = Q.resolveUrl('~/');
                            });
                        }
                    });
                });
            }
            SignUpPanel.prototype.getFormKey = function () { return Membership.SignUpForm.formKey; };
            SignUpPanel = __decorate([
                Serenity.Decorators.registerClass()
            ], SignUpPanel);
            return SignUpPanel;
        }(Serenity.PropertyPanel));
        Membership.SignUpPanel = SignUpPanel;
    })(Membership = CaseManagement.Membership || (CaseManagement.Membership = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var InboxDialog = (function (_super) {
            __extends(InboxDialog, _super);
            function InboxDialog() {
                _super.apply(this, arguments);
                this.form = new Messaging.InboxForm(this.idPrefix);
            }
            InboxDialog.prototype.getFormKey = function () { return Messaging.InboxForm.formKey; };
            InboxDialog.prototype.getIdProperty = function () { return Messaging.InboxRow.idProperty; };
            InboxDialog.prototype.getLocalTextPrefix = function () { return Messaging.InboxRow.localTextPrefix; };
            InboxDialog.prototype.getService = function () { return Messaging.InboxService.baseUrl; };
            InboxDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            InboxDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            InboxDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], InboxDialog);
            return InboxDialog;
        }(Serenity.EntityDialog));
        Messaging.InboxDialog = InboxDialog;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var InboxGrid = (function (_super) {
            __extends(InboxGrid, _super);
            function InboxGrid(container) {
                _super.call(this, container);
            }
            InboxGrid.prototype.getColumnsKey = function () { return 'Messaging.Inbox'; };
            InboxGrid.prototype.getDialogType = function () { return Messaging.InboxDialog; };
            InboxGrid.prototype.getIdProperty = function () { return Messaging.InboxRow.idProperty; };
            InboxGrid.prototype.getLocalTextPrefix = function () { return Messaging.InboxRow.localTextPrefix; };
            InboxGrid.prototype.getService = function () { return Messaging.InboxService.baseUrl; };
            InboxGrid.prototype.getButtons = function () {
                var buttons = _super.prototype.getButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            InboxGrid.prototype.getItemCssClass = function (item, index) {
                var klass = "";
                if ((item.Seen == null) || (item.Seen == false))
                    klass += "actionNotSeen";
                return Q.trimToNull(klass);
            };
            InboxGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], InboxGrid);
            return InboxGrid;
        }(Serenity.EntityGrid));
        Messaging.InboxGrid = InboxGrid;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var NewMessageDialog = (function (_super) {
            __extends(NewMessageDialog, _super);
            function NewMessageDialog() {
                _super.apply(this, arguments);
                this.form = new Messaging.NewMessageForm(this.idPrefix);
            }
            NewMessageDialog.prototype.getFormKey = function () { return Messaging.NewMessageForm.formKey; };
            NewMessageDialog.prototype.getIdProperty = function () { return Messaging.NewMessageRow.idProperty; };
            NewMessageDialog.prototype.getLocalTextPrefix = function () { return Messaging.NewMessageRow.localTextPrefix; };
            NewMessageDialog.prototype.getNameProperty = function () { return Messaging.NewMessageRow.nameProperty; };
            NewMessageDialog.prototype.getService = function () { return Messaging.NewMessageService.baseUrl; };
            NewMessageDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], NewMessageDialog);
            return NewMessageDialog;
        }(Serenity.EntityDialog));
        Messaging.NewMessageDialog = NewMessageDialog;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var NewMessageGrid = (function (_super) {
            __extends(NewMessageGrid, _super);
            function NewMessageGrid(container) {
                _super.call(this, container);
            }
            NewMessageGrid.prototype.getColumnsKey = function () { return 'Messaging.NewMessage'; };
            NewMessageGrid.prototype.getDialogType = function () { return Messaging.NewMessageDialog; };
            NewMessageGrid.prototype.getIdProperty = function () { return Messaging.NewMessageRow.idProperty; };
            NewMessageGrid.prototype.getLocalTextPrefix = function () { return Messaging.NewMessageRow.localTextPrefix; };
            NewMessageGrid.prototype.getService = function () { return Messaging.NewMessageService.baseUrl; };
            NewMessageGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], NewMessageGrid);
            return NewMessageGrid;
        }(Serenity.EntityGrid));
        Messaging.NewMessageGrid = NewMessageGrid;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var SentDialog = (function (_super) {
            __extends(SentDialog, _super);
            function SentDialog() {
                _super.apply(this, arguments);
                this.form = new Messaging.SentForm(this.idPrefix);
            }
            SentDialog.prototype.getFormKey = function () { return Messaging.SentForm.formKey; };
            SentDialog.prototype.getIdProperty = function () { return Messaging.SentRow.idProperty; };
            SentDialog.prototype.getLocalTextPrefix = function () { return Messaging.SentRow.localTextPrefix; };
            SentDialog.prototype.getService = function () { return Messaging.SentService.baseUrl; };
            SentDialog.prototype.getToolbarButtons = function () {
                var buttons = _super.prototype.getToolbarButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "save-and-close-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "apply-changes-button"; }), 1);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "delete-button"; }), 1);
                return buttons;
            };
            SentDialog.prototype.updateInterface = function () {
                _super.prototype.updateInterface.call(this);
                Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
                this.element.find('sup').hide();
                this.deleteButton.hide();
            };
            SentDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], SentDialog);
            return SentDialog;
        }(Serenity.EntityDialog));
        Messaging.SentDialog = SentDialog;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var SentGrid = (function (_super) {
            __extends(SentGrid, _super);
            function SentGrid(container) {
                _super.call(this, container);
            }
            SentGrid.prototype.getColumnsKey = function () { return 'Messaging.Sent'; };
            SentGrid.prototype.getDialogType = function () { return Messaging.SentDialog; };
            SentGrid.prototype.getIdProperty = function () { return Messaging.SentRow.idProperty; };
            SentGrid.prototype.getLocalTextPrefix = function () { return Messaging.SentRow.localTextPrefix; };
            SentGrid.prototype.getService = function () { return Messaging.SentService.baseUrl; };
            SentGrid.prototype.getButtons = function () {
                var buttons = _super.prototype.getButtons.call(this);
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            SentGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], SentGrid);
            return SentGrid;
        }(Serenity.EntityGrid));
        Messaging.SentGrid = SentGrid;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var VwMessagesDialog = (function (_super) {
            __extends(VwMessagesDialog, _super);
            function VwMessagesDialog() {
                _super.apply(this, arguments);
                this.form = new VwMessagesForm(this.idPrefix);
            }
            VwMessagesDialog.prototype.getFormKey = function () { return VwMessagesForm.formKey; };
            VwMessagesDialog.prototype.getLocalTextPrefix = function () { return Messaging.VwMessagesRow.localTextPrefix; };
            VwMessagesDialog.prototype.getNameProperty = function () { return Messaging.VwMessagesRow.nameProperty; };
            VwMessagesDialog.prototype.getService = function () { return Messaging.VwMessagesService.baseUrl; };
            VwMessagesDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], VwMessagesDialog);
            return VwMessagesDialog;
        }(Serenity.EntityDialog));
        Messaging.VwMessagesDialog = VwMessagesDialog;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Messaging;
    (function (Messaging) {
        var VwMessagesGrid = (function (_super) {
            __extends(VwMessagesGrid, _super);
            function VwMessagesGrid(container) {
                _super.call(this, container);
            }
            VwMessagesGrid.prototype.getColumnsKey = function () { return 'Messaging.VwMessages'; };
            VwMessagesGrid.prototype.getDialogType = function () { return Messaging.VwMessagesDialog; };
            VwMessagesGrid.prototype.getLocalTextPrefix = function () { return Messaging.VwMessagesRow.localTextPrefix; };
            VwMessagesGrid.prototype.getService = function () { return Messaging.VwMessagesService.baseUrl; };
            VwMessagesGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], VwMessagesGrid);
            return VwMessagesGrid;
        }(Serenity.EntityGrid));
        Messaging.VwMessagesGrid = VwMessagesGrid;
    })(Messaging = CaseManagement.Messaging || (CaseManagement.Messaging = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestDetailDialog = (function (_super) {
            __extends(ActivityRequestDetailDialog, _super);
            function ActivityRequestDetailDialog() {
                _super.apply(this, arguments);
                this.form = new StimulReport.ActivityRequestDetailForm(this.idPrefix);
            }
            ActivityRequestDetailDialog.prototype.getFormKey = function () { return StimulReport.ActivityRequestDetailForm.formKey; };
            ActivityRequestDetailDialog.prototype.getIdProperty = function () { return StimulReport.ActivityRequestDetailRow.idProperty; };
            ActivityRequestDetailDialog.prototype.getLocalTextPrefix = function () { return StimulReport.ActivityRequestDetailRow.localTextPrefix; };
            ActivityRequestDetailDialog.prototype.getService = function () { return StimulReport.ActivityRequestDetailService.baseUrl; };
            ActivityRequestDetailDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], ActivityRequestDetailDialog);
            return ActivityRequestDetailDialog;
        }(Serenity.EntityDialog));
        StimulReport.ActivityRequestDetailDialog = ActivityRequestDetailDialog;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestDetailGrid = (function (_super) {
            __extends(ActivityRequestDetailGrid, _super);
            function ActivityRequestDetailGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestDetailGrid.prototype.getColumnsKey = function () { return 'StimulReport.ActivityRequestDetail'; };
            ActivityRequestDetailGrid.prototype.getDialogType = function () { return StimulReport.ActivityRequestDetailDialog; };
            ActivityRequestDetailGrid.prototype.getIdProperty = function () { return StimulReport.ActivityRequestDetailRow.idProperty; };
            ActivityRequestDetailGrid.prototype.getLocalTextPrefix = function () { return StimulReport.ActivityRequestDetailRow.localTextPrefix; };
            ActivityRequestDetailGrid.prototype.getService = function () { return StimulReport.ActivityRequestDetailService.baseUrl; };
            ActivityRequestDetailGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage'),
                        new Slick.Aggregators.Sum('RecoverableLeakage'),
                        new Slick.Aggregators.Sum('Recovered')
                    ]
                });
                return grid;
            };
            ActivityRequestDetailGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestDetailGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: StimulReport.ActivityRequestDetailService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; //console.log(ActivityCode);
                        var CreateTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; //console.log(DiscoverTime_Start);
                        var CreateTime_End = AllFilters[3].getElementsByTagName('input')[1].value; //console.log(DiscoverTime_End);
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        var Cycle = document.getElementById("select2-chosen-2").innerHTML;
                        if (Cycle == null) {
                            Cycle = "";
                        } //console.log(Cycle);
                        window.location.href = "../Common/ActivityRequestDetailPrint?ActivityCode=" + ActivityCode + "&CreateTime_Start=" + CreateTime_Start
                            + "&CreateTime_End=" + CreateTime_End + "&Province=" + Province + "&Cycle=" + Cycle;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestDetailGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestDetailGrid);
            return ActivityRequestDetailGrid;
        }(Serenity.EntityGrid));
        StimulReport.ActivityRequestDetailGrid = ActivityRequestDetailGrid;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ActivityRequestReportGrid = (function (_super) {
            __extends(ActivityRequestReportGrid, _super);
            function ActivityRequestReportGrid(container) {
                _super.call(this, container);
            }
            ActivityRequestReportGrid.prototype.getColumnsKey = function () { return 'StimulReport.ActivityRequestReport'; };
            //protected getDialogType() { return ActivityRequestReportDialog; }
            ActivityRequestReportGrid.prototype.getIdProperty = function () { return StimulReport.ActivityRequestReportRow.idProperty; };
            ActivityRequestReportGrid.prototype.getService = function () { return StimulReport.ActivityRequestReportService.baseUrl; };
            ActivityRequestReportGrid.prototype.createSlickGrid = function () {
                var grid = _super.prototype.createSlickGrid.call(this);
                // need to register this plugin for grouping or you'll have errors
                grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());
                this.view.setSummaryOptions({
                    aggregators: [
                        new Slick.Aggregators.Sum('TotalLeakage'),
                        new Slick.Aggregators.Sum('RecoverableLeakage'),
                        new Slick.Aggregators.Sum('Recovered')
                    ]
                });
                return grid;
            };
            ActivityRequestReportGrid.prototype.getSlickOptions = function () {
                var opt = _super.prototype.getSlickOptions.call(this);
                opt.showFooterRow = true;
                return opt;
            };
            ActivityRequestReportGrid.prototype.usePager = function () {
                return false;
            };
            ActivityRequestReportGrid.prototype.getButtons = function () {
                var _this = this;
                var buttons = _super.prototype.getButtons.call(this);
                buttons.push(CaseManagement.Common.ExcelExportHelper.createToolButton({
                    grid: this,
                    service: StimulReport.ActivityRequestReportService.baseUrl + '/ListExcel',
                    onViewSubmit: function () { return _this.onViewSubmit(); },
                    separator: true
                }));
                buttons.push({
                    title: Q.text('چاپ'),
                    cssClass: 'print-preview-button',
                    onClick: function () {
                        var AllFilters = document.getElementsByClassName("quick-filter-item");
                        var ActivityCode = AllFilters[0].getElementsByTagName('input')[0].value; // console.log(ActivityCode);
                        var CreatedTime_Start = AllFilters[3].getElementsByTagName('input')[0].value; // console.log(CreatedTime_Start);
                        var CreatedTime_End = AllFilters[3].getElementsByTagName('input')[1].value; // console.log(CreatedTime_End);
                        var DiscoverTime_Start = AllFilters[4].getElementsByTagName('input')[0].value; // console.log(DiscoverTime_Start);
                        var DiscoverTime_End = AllFilters[4].getElementsByTagName('input')[1].value; // console.log(DiscoverTime_End);
                        var TotalLeakage_From = AllFilters[5].getElementsByTagName('input')[0].value; // console.log(TotalLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var TotalLeakage_To = AllFilters[5].getElementsByTagName('input')[1].value; //console.log(TotalLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var RecoverableLeakage_From = AllFilters[6].getElementsByTagName('input')[0].value; // console.log(RecoverableLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var RecoverableLeakage_To = AllFilters[6].getElementsByTagName('input')[1].value; // console.log(RecoverableLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var Recovered_From = AllFilters[7].getElementsByTagName('input')[0].value; // console.log(Recovered_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var Recovered_To = AllFilters[7].getElementsByTagName('input')[1].value; // console.log(Recovered_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1"));
                        var Province = document.getElementById("select2-chosen-1").innerHTML;
                        if (Province == null) {
                            Province = "";
                        } //console.log(Province);
                        /*   var Cycle = document.getElementById("select2-chosen-2").innerHTML;
       
                           if (Cycle == null) { Cycle = ""; } //console.log(Cycle);*/
                        var IncomeFlow = document.getElementById("select2-chosen-2").innerHTML;
                        if (IncomeFlow == null) {
                            IncomeFlow = "";
                        } //console.log(IncomeFlow);
                        window.location.href = "../Common/ActivityRequestReportPrint?ActivityCode=" + ActivityCode + "&CreatedTime_Start=" + CreatedTime_Start + "&CreatedTime_End=" + CreatedTime_End
                            + "&DiscoverTime_Start=" + DiscoverTime_Start + "&DiscoverTime_End=" + DiscoverTime_End + "&TotalLeakage_From=" + TotalLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&TotalLeakage_To="
                            + TotalLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&RecoverableLeakage_From=" + RecoverableLeakage_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1")
                            + "&RecoverableLeakage_To=" + RecoverableLeakage_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&Recovered_From=" + Recovered_From.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1")
                            + "&Recovered_To=" + Recovered_To.replace(/(\d+),(?=\d{3}(\D|$))/g, "$1") + "&Province=" + Province + "&IncomeFlow=" + IncomeFlow;
                    }
                });
                buttons.splice(Q.indexOf(buttons, function (x) { return x.cssClass == "add-button"; }), 1);
                return buttons;
            };
            ActivityRequestReportGrid.prototype.getQuickFilters = function () {
                var filters = _super.prototype.getQuickFilters.call(this);
                var fld = CaseManagement.Case.ActivityRequestRow.Fields;
                var endTotalLeakage = null;
                filters.push({
                    field: fld.TotalLeakage,
                    type: Serenity.DecimalEditor,
                    title: 'مبلغ نشتی کل از تا',
                    element: function (e1) {
                        e1.css("width", "80px");
                        endTotalLeakage = Serenity.Widget.create({
                            type: Serenity.DecimalEditor,
                            element: function (e2) { return e2.insertAfter(e1).css("width", "80px"); }
                        });
                        endTotalLeakage.element.change(function (x) { return e1.triggerHandler("change"); });
                        $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                    },
                    handler: function (h) {
                        var active1 = h.value != null && !isNaN(h.value);
                        var active2 = endTotalLeakage.value != null && !isNaN(endTotalLeakage.value);
                        h.active = active1 || active2;
                        if (active1)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.TotalLeakage], '>=', h.value]);
                        if (active2)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.TotalLeakage], '<=', endTotalLeakage.value]);
                    }
                });
                var endRecoverableLeakage = null;
                filters.push({
                    field: fld.RecoverableLeakage,
                    type: Serenity.DecimalEditor,
                    title: 'مبلغ نشتی قابل وصول از تا',
                    element: function (e1) {
                        e1.css("width", "80px");
                        endRecoverableLeakage = Serenity.Widget.create({
                            type: Serenity.DecimalEditor,
                            element: function (e2) { return e2.insertAfter(e1).css("width", "80px"); }
                        });
                        endRecoverableLeakage.element.change(function (x) { return e1.triggerHandler("change"); });
                        $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                    },
                    handler: function (h) {
                        var active1 = h.value != null && !isNaN(h.value);
                        var active2 = endRecoverableLeakage.value != null && !isNaN(endRecoverableLeakage.value);
                        h.active = active1 || active2;
                        if (active1)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.RecoverableLeakage], '>=', h.value]);
                        if (active2)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.RecoverableLeakage], '<=', endRecoverableLeakage.value]);
                    }
                });
                var endRecovered = null;
                filters.push({
                    field: fld.Recovered,
                    type: Serenity.DecimalEditor,
                    title: 'مبلغ مصوب از تا',
                    element: function (e1) {
                        e1.css("width", "80px");
                        endRecovered = Serenity.Widget.create({
                            type: Serenity.DecimalEditor,
                            element: function (e2) { return e2.insertAfter(e1).css("width", "80px"); }
                        });
                        endRecovered.element.change(function (x) { return e1.triggerHandler("change"); });
                        $("<span/>").addClass("range-separator").text("-").insertAfter(e1);
                    },
                    handler: function (h) {
                        var active1 = h.value != null && !isNaN(h.value);
                        var active2 = endRecovered.value != null && !isNaN(endRecovered.value);
                        h.active = active1 || active2;
                        if (active1)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.Recovered], '>=', h.value]);
                        if (active2)
                            h.request.Criteria = Serenity.Criteria.and(h.request.Criteria, [[fld.Recovered], '<=', endRecovered.value]);
                    }
                });
                return filters;
            };
            ActivityRequestReportGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], ActivityRequestReportGrid);
            return ActivityRequestReportGrid;
        }(Serenity.EntityGrid));
        StimulReport.ActivityRequestReportGrid = ActivityRequestReportGrid;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var Groups = new Array();
        var Provinces = new Array();
        var ProvinceActivities = new Array();
        //var PROVINCEACTIVITYCOUNT = 0;
        var ProvinceActivityReport = (function (_super) {
            __extends(ProvinceActivityReport, _super);
            function ProvinceActivityReport() {
                _super.apply(this, arguments);
            }
            ProvinceActivityReport.ProvinceActivityGroupList = function () {
                var SetactivityGroupList = CaseManagement.Case.ActivityService.ActivitybyGroupList({}, function (response) {
                    var combo = document.getElementById("ActivityGroup");
                    for (var i = 0; i < response.Values.length; i++) {
                        Groups.push(new ActivityGroup(response.Values[i].GroupId, response.Values[i].Code, response.Values[i].Name));
                        //   console.log("ID :" + Groups[i]['GroupId'] + "Code :" + Groups[i]['Codes']);
                        var option = document.createElement("option");
                        option.text = response.Values[i].GroupName;
                        option.value = response.Values[i].GroupId;
                        combo.add(option, null); //Standard 
                    }
                });
                var Excludecols;
                Excludecols = ["EnglishName", "ManagerName", "LetterNo", "PmoLevelId", "InstallDate", "CreatedUserId", "CreatedDate", "ModifiedUserId", "ModifiedDate",
                    "IsDeleted", "DeletedUserId", "'DeletedDate", "PmoLevelName", "LeaderName"];
                var SetProvinceList = CaseManagement.Case.ProvinceService.List({ ExcludeColumns: Excludecols, "Sort": ["Id"] }, function (response) {
                    for (var i = 0; i < response.TotalCount; i++) {
                        Provinces.push(new Province(response.Entities[i].Id, response.Entities[i].Name));
                    }
                });
            };
            ProvinceActivityReport.ProvinceActivityList = function (GID) {
                var PROVINCEACTIVITYCOUNT = 0;
                var GroupCodes = Groups[GID - 1]['Codes'];
                var GroupNames = Groups[GID - 1]['Names'];
                // var P_Avtivity = _.groupBy(ProvinceActivity, 'ProvinceId', 'Code');
                //console.log(GroupCodes.length);
                //console.log(GroupNames.length);
                ProvinceActivities = [];
                var Excludecols;
                Excludecols = ["CycleCost", "Factor", "DelayedCost", "YearCost", "AccessibleCost", "InaccessibleCost", "Financial", "TotalLeakage", "RecoverableLeakage",
                    "Recovered", "EventDescription", "MainReason", "CorrectionOperation", "AvoidRepeatingOperation", "CreatedUserId", "CreatedDate", "ModifiedUserId", "ModifiedDate",
                    "IsDeleted", "CycleId", "CustomerSelectedId", "CustomerEffectId", "OrganizationEffectId", "IncomeFlowId", "RiskLevelId", "StatusID", "DiscoverLeakDate", "ActionId", "ConfirmTypeID",
                    "CommentReasonList"];
                var aaa = CaseManagement.Case.ActivityRequestService.List({ ExcludeColumns: Excludecols }, function (response) {
                    for (var RequestCounter = 0; RequestCounter < response.TotalCount; RequestCounter++) {
                        if (String(response.Entities[RequestCounter].ActivityCode).charAt(1) === String(GID)) {
                            ProvinceActivities.push(new ProvinceActivity(response.Entities[RequestCounter].ProvinceId, response.Entities[RequestCounter].ActivityCode, 1));
                        }
                    }
                    // console.log("ProvinceActivities length : " + ProvinceActivities.length);
                    /*  var unique = ProvinceActivities.filter(function (elem, index, self) {
                          return index == self.indexOf(elem);
                      });
                      console.log("Length after Process : " + unique.length);
                      */
                    /* for (var PAcounter = 0; PAcounter < ProvinceActivities.length; PAcounter++)
                     {
     
                     }*/
                    //console.log(response.TotalCount);
                    var TableHead = document.getElementById('Province_Activity_Table').getElementsByTagName('thead')[0];
                    var TableBody = document.getElementById('Province_Activity_Table').getElementsByTagName('tbody')[0];
                    TableHead.innerHTML = '';
                    TableBody.innerHTML = '';
                    var newRow = TableHead.insertRow(TableHead.rows.length);
                    var th = document.createElement('th');
                    th.style = "text-align:center;font-size:11px;";
                    th.innerHTML = 'استان';
                    newRow.appendChild(th);
                    for (var GCcounter = 0; GCcounter < GroupCodes.length; GCcounter++) {
                        var th = document.createElement('th');
                        th.style = "text-align:center";
                        th.innerHTML = GroupCodes[GCcounter];
                        newRow.appendChild(th);
                    }
                    for (var counter = 0; counter < Provinces.length; counter++) {
                        //console.log("PROVINCE LOOP");
                        //PROVINCEACTIVITYCOUNT = 0;
                        var newRow1 = TableBody.insertRow(TableBody.rows.length);
                        var newCell = newRow1.insertCell(0);
                        // Append a text node to the cell
                        newCell.style = "text-align:center,font-size:11px;";
                        var newText = document.createTextNode(Provinces[counter]['Name']);
                        newCell.appendChild(newText);
                        newRow1.appendChild(newCell);
                        for (var counter1 = 0; counter1 < GroupCodes.length; counter1++) {
                            //  console.log("GROUPCODES");
                            var cellIndex = counter1 + 1;
                            var newCell1 = newRow1.insertCell(cellIndex);
                            // Append a text node to the cell
                            newCell1.style = "text-align:center;font-size:13px;font-weight: bold";
                            var flag = false;
                            var PActivitiesCounter = 0;
                            for (var counter2 = 0; counter2 < ProvinceActivities.length; counter2++) {
                                //console.log("PROVINCEACTIVITY LOOP");
                                //console.log("Province : " + ProvinceActivities[counter2]['ProvinceId'] + ' --- ' + Provinces[counter]['Id']);
                                // console.log("Activity : " + ProvinceActivities[counter2]['Code'].length + ' --- ' + GroupCodes[counter1].length + ' --- After Trim : ' + GroupCodes[counter1].trim().length);
                                if ((ProvinceActivities[counter2]['Code'] == GroupCodes[counter1].trim()) && (ProvinceActivities[counter2]['ProvinceId'] == Provinces[counter]['Id'])) {
                                    PActivitiesCounter += 1;
                                    var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
                                    var chars = PActivitiesCounter.toString().split('');
                                    for (var i = 0; i < chars.length; i++) {
                                        if (/\d/.test(chars[i])) {
                                            chars[i] = arabicNumbers[chars[i]];
                                        }
                                    }
                                    var count = chars.join('');
                                    var newText1 = document.createTextNode(count.toString());
                                    newCell1.setAttribute('id', ProvinceActivities[counter2]['ProvinceId'] + "_" + GroupCodes[counter1]);
                                    newCell1.title = GroupNames[counter1];
                                    flag = true;
                                }
                            }
                            PROVINCEACTIVITYCOUNT = 0;
                            if (!flag) {
                                var newText1 = document.createTextNode("");
                            }
                            newCell1.appendChild(newText1);
                            newRow1.appendChild(newCell1);
                        }
                    }
                });
            };
            return ProvinceActivityReport;
        }(Serenity.TemplatedDialog));
        StimulReport.ProvinceActivityReport = ProvinceActivityReport;
        var ActivityGroup = (function () {
            function ActivityGroup(Id, code, name) {
                this.Id = Id;
                this.code = code;
                this.name = name;
                this.GroupId = Id;
                this.Codes = code;
                this.Names = name;
            }
            return ActivityGroup;
        }());
        var Province = (function () {
            function Province(ID, name) {
                this.ID = ID;
                this.name = name;
                this.Id = ID;
                this.Name = name;
            }
            return Province;
        }());
        var ProvinceActivity = (function () {
            function ProvinceActivity(Id, code, count) {
                this.Id = Id;
                this.code = code;
                this.count = count;
                this.ProvinceId = Id;
                this.Code = code;
                this.Count = count;
            }
            return ProvinceActivity;
        }());
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var ProvinceLineChart = (function (_super) {
            __extends(ProvinceLineChart, _super);
            function ProvinceLineChart() {
                _super.apply(this, arguments);
            }
            ProvinceLineChart.ProvinceProgram96 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport96({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            ProvinceLineChart.ProvinceProgram95 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            ProvinceLineChart.ProvinceProgram94 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport94({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            ProvinceLineChart.ProvinceProgram93 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport93({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            ProvinceLineChart.ProvinceProgram92 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport92({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            ProvinceLineChart = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], ProvinceLineChart);
            return ProvinceLineChart;
        }(Serenity.TemplatedDialog));
        StimulReport.ProvinceLineChart = ProvinceLineChart;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var UserLeaderActivityDetail = (function (_super) {
            __extends(UserLeaderActivityDetail, _super);
            function UserLeaderActivityDetail() {
                _super.apply(this, arguments);
            }
            UserLeaderActivityDetail.initializePage = function () {
                var aaa = CaseManagement.Administration.LogService.LogLeaderReport({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'UserLeaderChart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Count'],
                        labels: ['تعداد'],
                        hideHover: 'auto'
                    });
                });
            };
            UserLeaderActivityDetail = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], UserLeaderActivityDetail);
            return UserLeaderActivityDetail;
        }(Serenity.TemplatedDialog));
        StimulReport.UserLeaderActivityDetail = UserLeaderActivityDetail;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var UserMonthActivityDetail = (function (_super) {
            __extends(UserMonthActivityDetail, _super);
            function UserMonthActivityDetail() {
                _super.apply(this, arguments);
            }
            UserMonthActivityDetail.initializePage = function () {
                // var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport({}, response => {
                //
                //     var bar = new Morris.Bar({
                //         element: 'province-bar-chart',
                //         resize: true,
                //         parseTime: false,
                //         data: response.Values,
                //         xkey: 'Provinve',
                //         ykeys: ['Program', 'Leak', 'Confirm'],
                //         labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                //         hideHover: 'auto'
                //     });
                // });            
            };
            UserMonthActivityDetail = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], UserMonthActivityDetail);
            return UserMonthActivityDetail;
        }(Serenity.TemplatedDialog));
        StimulReport.UserMonthActivityDetail = UserMonthActivityDetail;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var StimulReport;
    (function (StimulReport) {
        var UserProvinceActivityDetail = (function (_super) {
            __extends(UserProvinceActivityDetail, _super);
            function UserProvinceActivityDetail() {
                _super.apply(this, arguments);
            }
            UserProvinceActivityDetail.initializePage = function () {
                var aaa = CaseManagement.Administration.LogService.LogProvinceReport({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'ProvinceChart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Count'],
                        labels: ['تعداد'],
                        hideHover: 'auto'
                    });
                });
            };
            UserProvinceActivityDetail = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], UserProvinceActivityDetail);
            return UserProvinceActivityDetail;
        }(Serenity.TemplatedDialog));
        StimulReport.UserProvinceActivityDetail = UserProvinceActivityDetail;
    })(StimulReport = CaseManagement.StimulReport || (CaseManagement.StimulReport = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowActionDialog = (function (_super) {
            __extends(WorkFlowActionDialog, _super);
            function WorkFlowActionDialog() {
                _super.apply(this, arguments);
                this.form = new WorkFlow.WorkFlowActionForm(this.idPrefix);
            }
            WorkFlowActionDialog.prototype.getFormKey = function () { return WorkFlow.WorkFlowActionForm.formKey; };
            WorkFlowActionDialog.prototype.getIdProperty = function () { return WorkFlow.WorkFlowActionRow.idProperty; };
            WorkFlowActionDialog.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowActionRow.localTextPrefix; };
            WorkFlowActionDialog.prototype.getNameProperty = function () { return WorkFlow.WorkFlowActionRow.nameProperty; };
            WorkFlowActionDialog.prototype.getService = function () { return WorkFlow.WorkFlowActionService.baseUrl; };
            WorkFlowActionDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], WorkFlowActionDialog);
            return WorkFlowActionDialog;
        }(Serenity.EntityDialog));
        WorkFlow.WorkFlowActionDialog = WorkFlowActionDialog;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowActionGrid = (function (_super) {
            __extends(WorkFlowActionGrid, _super);
            function WorkFlowActionGrid(container) {
                _super.call(this, container);
            }
            WorkFlowActionGrid.prototype.getColumnsKey = function () { return 'WorkFlow.WorkFlowAction'; };
            WorkFlowActionGrid.prototype.getDialogType = function () { return WorkFlow.WorkFlowActionDialog; };
            WorkFlowActionGrid.prototype.getIdProperty = function () { return WorkFlow.WorkFlowActionRow.idProperty; };
            WorkFlowActionGrid.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowActionRow.localTextPrefix; };
            WorkFlowActionGrid.prototype.getService = function () { return WorkFlow.WorkFlowActionService.baseUrl; };
            WorkFlowActionGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], WorkFlowActionGrid);
            return WorkFlowActionGrid;
        }(Serenity.EntityGrid));
        WorkFlow.WorkFlowActionGrid = WorkFlowActionGrid;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowRuleDialog = (function (_super) {
            __extends(WorkFlowRuleDialog, _super);
            function WorkFlowRuleDialog() {
                _super.apply(this, arguments);
                this.form = new WorkFlow.WorkFlowRuleForm(this.idPrefix);
            }
            WorkFlowRuleDialog.prototype.getFormKey = function () { return WorkFlow.WorkFlowRuleForm.formKey; };
            WorkFlowRuleDialog.prototype.getIdProperty = function () { return WorkFlow.WorkFlowRuleRow.idProperty; };
            WorkFlowRuleDialog.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowRuleRow.localTextPrefix; };
            WorkFlowRuleDialog.prototype.getService = function () { return WorkFlow.WorkFlowRuleService.baseUrl; };
            WorkFlowRuleDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], WorkFlowRuleDialog);
            return WorkFlowRuleDialog;
        }(Serenity.EntityDialog));
        WorkFlow.WorkFlowRuleDialog = WorkFlowRuleDialog;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowRuleGrid = (function (_super) {
            __extends(WorkFlowRuleGrid, _super);
            function WorkFlowRuleGrid(container) {
                _super.call(this, container);
            }
            WorkFlowRuleGrid.prototype.getColumnsKey = function () { return 'WorkFlow.WorkFlowRule'; };
            WorkFlowRuleGrid.prototype.getDialogType = function () { return WorkFlow.WorkFlowRuleDialog; };
            WorkFlowRuleGrid.prototype.getIdProperty = function () { return WorkFlow.WorkFlowRuleRow.idProperty; };
            WorkFlowRuleGrid.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowRuleRow.localTextPrefix; };
            WorkFlowRuleGrid.prototype.getService = function () { return WorkFlow.WorkFlowRuleService.baseUrl; };
            WorkFlowRuleGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], WorkFlowRuleGrid);
            return WorkFlowRuleGrid;
        }(Serenity.EntityGrid));
        WorkFlow.WorkFlowRuleGrid = WorkFlowRuleGrid;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusDialog = (function (_super) {
            __extends(WorkFlowStatusDialog, _super);
            function WorkFlowStatusDialog() {
                _super.apply(this, arguments);
                this.form = new WorkFlow.WorkFlowStatusForm(this.idPrefix);
            }
            WorkFlowStatusDialog.prototype.getFormKey = function () { return WorkFlow.WorkFlowStatusForm.formKey; };
            WorkFlowStatusDialog.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStatusRow.idProperty; };
            WorkFlowStatusDialog.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStatusRow.localTextPrefix; };
            WorkFlowStatusDialog.prototype.getNameProperty = function () { return WorkFlow.WorkFlowStatusRow.nameProperty; };
            WorkFlowStatusDialog.prototype.getService = function () { return WorkFlow.WorkFlowStatusService.baseUrl; };
            WorkFlowStatusDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], WorkFlowStatusDialog);
            return WorkFlowStatusDialog;
        }(Serenity.EntityDialog));
        WorkFlow.WorkFlowStatusDialog = WorkFlowStatusDialog;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusGrid = (function (_super) {
            __extends(WorkFlowStatusGrid, _super);
            function WorkFlowStatusGrid(container) {
                _super.call(this, container);
            }
            WorkFlowStatusGrid.prototype.getColumnsKey = function () { return 'WorkFlow.WorkFlowStatus'; };
            WorkFlowStatusGrid.prototype.getDialogType = function () { return WorkFlow.WorkFlowStatusDialog; };
            WorkFlowStatusGrid.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStatusRow.idProperty; };
            WorkFlowStatusGrid.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStatusRow.localTextPrefix; };
            WorkFlowStatusGrid.prototype.getService = function () { return WorkFlow.WorkFlowStatusService.baseUrl; };
            WorkFlowStatusGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], WorkFlowStatusGrid);
            return WorkFlowStatusGrid;
        }(Serenity.EntityGrid));
        WorkFlow.WorkFlowStatusGrid = WorkFlowStatusGrid;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusTypeDialog = (function (_super) {
            __extends(WorkFlowStatusTypeDialog, _super);
            function WorkFlowStatusTypeDialog() {
                _super.apply(this, arguments);
                this.form = new WorkFlow.WorkFlowStatusTypeForm(this.idPrefix);
            }
            WorkFlowStatusTypeDialog.prototype.getFormKey = function () { return WorkFlow.WorkFlowStatusTypeForm.formKey; };
            WorkFlowStatusTypeDialog.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStatusTypeRow.idProperty; };
            WorkFlowStatusTypeDialog.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStatusTypeRow.localTextPrefix; };
            WorkFlowStatusTypeDialog.prototype.getNameProperty = function () { return WorkFlow.WorkFlowStatusTypeRow.nameProperty; };
            WorkFlowStatusTypeDialog.prototype.getService = function () { return WorkFlow.WorkFlowStatusTypeService.baseUrl; };
            WorkFlowStatusTypeDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], WorkFlowStatusTypeDialog);
            return WorkFlowStatusTypeDialog;
        }(Serenity.EntityDialog));
        WorkFlow.WorkFlowStatusTypeDialog = WorkFlowStatusTypeDialog;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStatusTypeGrid = (function (_super) {
            __extends(WorkFlowStatusTypeGrid, _super);
            function WorkFlowStatusTypeGrid(container) {
                _super.call(this, container);
            }
            WorkFlowStatusTypeGrid.prototype.getColumnsKey = function () { return 'WorkFlow.WorkFlowStatusType'; };
            WorkFlowStatusTypeGrid.prototype.getDialogType = function () { return WorkFlow.WorkFlowStatusTypeDialog; };
            WorkFlowStatusTypeGrid.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStatusTypeRow.idProperty; };
            WorkFlowStatusTypeGrid.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStatusTypeRow.localTextPrefix; };
            WorkFlowStatusTypeGrid.prototype.getService = function () { return WorkFlow.WorkFlowStatusTypeService.baseUrl; };
            WorkFlowStatusTypeGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], WorkFlowStatusTypeGrid);
            return WorkFlowStatusTypeGrid;
        }(Serenity.EntityGrid));
        WorkFlow.WorkFlowStatusTypeGrid = WorkFlowStatusTypeGrid;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStepDialog = (function (_super) {
            __extends(WorkFlowStepDialog, _super);
            function WorkFlowStepDialog() {
                _super.apply(this, arguments);
                this.form = new WorkFlow.WorkFlowStepForm(this.idPrefix);
            }
            WorkFlowStepDialog.prototype.getFormKey = function () { return WorkFlow.WorkFlowStepForm.formKey; };
            WorkFlowStepDialog.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStepRow.idProperty; };
            WorkFlowStepDialog.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStepRow.localTextPrefix; };
            WorkFlowStepDialog.prototype.getNameProperty = function () { return WorkFlow.WorkFlowStepRow.nameProperty; };
            WorkFlowStepDialog.prototype.getService = function () { return WorkFlow.WorkFlowStepService.baseUrl; };
            WorkFlowStepDialog = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.responsive()
            ], WorkFlowStepDialog);
            return WorkFlowStepDialog;
        }(Serenity.EntityDialog));
        WorkFlow.WorkFlowStepDialog = WorkFlowStepDialog;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var WorkFlow;
    (function (WorkFlow) {
        var WorkFlowStepGrid = (function (_super) {
            __extends(WorkFlowStepGrid, _super);
            function WorkFlowStepGrid(container) {
                _super.call(this, container);
            }
            WorkFlowStepGrid.prototype.getColumnsKey = function () { return 'WorkFlow.WorkFlowStep'; };
            WorkFlowStepGrid.prototype.getDialogType = function () { return WorkFlow.WorkFlowStepDialog; };
            WorkFlowStepGrid.prototype.getIdProperty = function () { return WorkFlow.WorkFlowStepRow.idProperty; };
            WorkFlowStepGrid.prototype.getLocalTextPrefix = function () { return WorkFlow.WorkFlowStepRow.localTextPrefix; };
            WorkFlowStepGrid.prototype.getService = function () { return WorkFlow.WorkFlowStepService.baseUrl; };
            WorkFlowStepGrid = __decorate([
                Serenity.Decorators.registerClass()
            ], WorkFlowStepGrid);
            return WorkFlowStepGrid;
        }(Serenity.EntityGrid));
        WorkFlow.WorkFlowStepGrid = WorkFlowStepGrid;
    })(WorkFlow = CaseManagement.WorkFlow || (CaseManagement.WorkFlow = {}));
})(CaseManagement || (CaseManagement = {}));
var CaseManagement;
(function (CaseManagement) {
    var Common;
    (function (Common) {
        var DashboardIndex = (function (_super) {
            __extends(DashboardIndex, _super);
            function DashboardIndex() {
                _super.apply(this, arguments);
            }
            DashboardIndex.ProvinceProgram95 = function () {
                var aaa = CaseManagement.Case.ProvinceProgramService.ProvinceProgramLineReport({}, function (response) {
                    var bar = new Morris.Bar({
                        element: 'province-bar-chart',
                        resize: true,
                        parseTime: false,
                        data: response.Values,
                        xkey: 'Provinve',
                        ykeys: ['Program', 'Leak', 'Confirm'],
                        labels: ['برنامه', 'نشتی اولیه', 'نشتی تایید شده'],
                        hideHover: 'auto'
                    });
                });
            };
            DashboardIndex = __decorate([
                Serenity.Decorators.registerClass(),
                Serenity.Decorators.resizable(),
                Serenity.Decorators.maximizable()
            ], DashboardIndex);
            return DashboardIndex;
        }(Serenity.TemplatedDialog));
        Common.DashboardIndex = DashboardIndex;
    })(Common = CaseManagement.Common || (CaseManagement.Common = {}));
})(CaseManagement || (CaseManagement = {}));
//# sourceMappingURL=CaseManagement.Web.js.map