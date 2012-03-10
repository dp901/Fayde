/// <reference path="../../Runtime/Nullstone.js" />
/// <reference path="DependencyObjectCollection.js"/>
/// CODE

//#region UIElementCollection

function UIElementCollection() {
    if (!Nullstone.IsReady)
        return;
    this.$super();
    this._ZSorted = new Array();
}
Nullstone.Extend(UIElementCollection, "UIElementCollection", DependencyObjectCollection);

UIElementCollection.prototype.GetValueAtZIndex = function (index) {
    return this._ZSorted[index];
};
UIElementCollection.prototype.GetZSortedCount = function () {
    return this._ZSorted.length;
};
UIElementCollection.prototype.ResortByZIndex = function () {
    var count = this.GetCount();
    this._ZSorted = new Array(count);
    if (count < 1)
        return;

    for (var i = 0; i < count; i++) {
        this._ZSorted[i] = this._ht[i];
    }

    if (count > 1) {
        this._ZSorted.sort(UIElement.ZIndexComparer);
    }
};
UIElementCollection.prototype.IsElementType = function (value) {
    return value instanceof UIElement;
};

//#endregion
