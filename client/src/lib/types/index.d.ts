type Activity = {
  id: string
  title: string
  date: Date
  description: string
  category: string
  isCancelled: boolean
  city: string
  venue: string
  latitude: number
  longitude: number
  attendees: Profile[]
  isGoing: boolean
  isHost: boolean
  hostId: string
  hostDisplayName: string
}

interface Profile {
  id: string,
  displayName: string
  bio?: string
  imageUrl?: string
}

interface User {
  id: string
  email: string
  displayName: string
  imageUrl?: string
}

interface LocationIQSuggestion {
  place_id: string
  osm_id: string
  osm_type: string
  license: string
  lat: string
  lon: string
  boundingbox: string[]
  class: string
  type: string
  display_name: string
  display_place: string
  display_address: string
  address: Address
}

interface LocationIQAddress {
  name: string
  state: string
  country: string
  country_code: string
  house_number?: string
  road?: string
  suburb?: string
  city?: string
  postcode?: string
}