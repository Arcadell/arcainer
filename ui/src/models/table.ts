export class TableRow {
    id!: string;
    selected!: boolean;
    fields!: any
}

export class TableField {
    isId!: boolean;
    label!: string;
    key!: string;
}