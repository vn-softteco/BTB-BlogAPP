import Cookies from 'js-cookie'

const setToken = (token: string) => {
  Cookies.set('token', token)
}

const deleteToken = () => {
  Cookies.remove('token')
}

const getAccessToken = () => Cookies.get('token')

export default {
    setToken,
    deleteToken,
    getAccessToken
}
