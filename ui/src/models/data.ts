export class BaseEntity {
    id!: string;
}

export class Container extends BaseEntity {
    name!: string;
    state!: string;
}

export enum ContainerCommands {
    Start,
    Stop,
    Delete
}

export class Image extends BaseEntity {}

export class Network extends BaseEntity {
    name!: string;
}

export class Volume {
    name!: string;
}