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

export class CreateContainerCommand {
    name!: string;
    compose!: string;
    startOnCreate: boolean = false;
}

export class Image extends BaseEntity { }

export class Network extends BaseEntity {
    name!: string;
}

export class Volume {
    name!: string;
}

export class Stack {
    name!: string;
    dockerCompose!: string;
}

export class Settings {
    id!: string;
    disableRegistration!: boolean
}