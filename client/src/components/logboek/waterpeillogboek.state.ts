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
    password: string
    addressId: string
    address: any
  }

  export interface MiniPC {
    uuid: string
    key: string
    status: boolean
  }

export interface WaterpeilLogboekState {
    date: string;
    address?: string;
    level: string;
}