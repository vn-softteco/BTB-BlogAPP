export const ROUTES = {
    INITIAL_ROUTE: '/',
    SIGN_IN: '/sign-in',
    SIGN_UP: '/sign-up',
    GET_BLOGPOSTs: '/blog-posts',
    BLOGPOST_BY_ID: '/blog-posts/:id',
    GET_BLOGPOST_BY_ID: (id: string) => `/blog-posts/${id}`,
    ADD_BLOGPOST: '/blog-posts/add-blog-post',
    POST_ADD_COMMENT: (id: string) => `/blog-posts/add-comment/${id}`,
    ADD_COMMENT: '/blog-posts/add-comment/:id'
}

export const API_ENDPOINTS = {
    SIGN_UP: 'auth/register',
    SIGN_IN: 'auth/login',
    GET_ALL_BLOG_POSTS: 'blogpost',
    ADD_BLOG_POST: 'blogpost',
    ADD_COMMENT: 'comment',
    GET_ALL_BLOG_POST_BY_ID: (id: string) => `/blogpost/${id}`,
}