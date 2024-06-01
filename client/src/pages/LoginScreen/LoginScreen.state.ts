export interface LoginTokenResponse {
    token: string;
}
export interface UserResponse {
    id: string
    firstName: string
    lastName: string
    email: string
    address: Address
    waterlevelsettings: WaterLevelSettings
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

  export interface WaterLevelSettings {
    id: string
    userId: string
    poleheight: number
    idealheight: number
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
    waterlevelsettings: any
  }
  