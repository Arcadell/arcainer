export class BaseEntity {
    id!: string;
}

export class Container extends BaseEntity {
    name!: string;
    state!: string;
}