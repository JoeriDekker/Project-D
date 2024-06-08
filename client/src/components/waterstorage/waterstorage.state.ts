export interface WaterStorageState {

    typeStorage: string;
    waterStored: string;
    storageState: string;

}

export interface UserResponse {
    id: string
    firstName: string
    lastName: string
    email: string
    address: Address
}

export interface Address {
    id: string
    street: string
    houseNumber: string
    city: string
    zip: string
    userId: string
    user: User
}

export interface User {
    id: string
    firstName: string
    lastName: string
    email: string
    addressId: string
    miniPCs: MiniPC
}

export interface MiniPC {
    uuid: string
    key: string
    status: boolean
}