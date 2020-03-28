export interface Page {
    pageSize: number;
    pageNumber: number;
    sortFields: string;
    keyword: string;
    total: number;
}

// tslint:disable-next-line: no-empty-interface
export class PageRequest implements Page {
    constructor(_pageSize: number = 10,
        _pageNumber: number = 1,
        _sortFields: string = 'Id',
        _keyword: string = null,
        _total: number = 0) {
        this.pageSize = _pageSize;
        this.pageNumber = _pageNumber;
        this.keyword = _keyword;
        this.sortFields = _sortFields;
        this.total = _total;
    }
    pageSize: number;
    pageNumber: number;
    sortFields: string;
    keyword: string;
    total: number;
}

export interface PageView<T> extends Page {
    datas: T[];
}

export class PageView<T> implements PageView<T> {
    constructor(_datas: T[] = [], _page: Page = new PageRequest()) {
        this.datas = _datas;
        this.keyword = _page.keyword;
        this.pageNumber = _page.pageNumber;
        this.pageSize = _page.pageSize;
        this.total = _page.total;
        this.sortFields = _page.sortFields;
    }
}


