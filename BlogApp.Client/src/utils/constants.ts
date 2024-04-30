export const ROUTES = {
    INITIAL_ROUTE: '/',
    SIGN_IN: '/sign-in',
    SIGN_UP: '/sign-up',
    GET_BLOGPOSTs: '/blog-posts',
    BLOGPOST_BY_ID: '/blog-posts/:id',
    GET_BLOGPOST_BY_ID: (id: string) => `/blog-posts/${id}`,
    ADD_BLOGPOST: '/blog-posts/add-blog-post',
    EDIT_BLOGPOST: '/blog-posts/edit-blog-post',
    ROUTE_ADD_COMMENT: (id: string) => `/blog-posts/add-comment/${id}`,
    ADD_COMMENT: '/blog-posts/add-comment/:id',
    ROUTE_UPDATE_COMMENT: (id: string) => `/blog-posts/update-comment/${id}`,
    UPDATE_COMMENT: '/blog-posts/update-comment/:id'
}

export const API_ENDPOINTS = {
    SIGN_UP: 'auth/register',
    SIGN_IN: 'auth/login',
    GET_ALL_BLOG_POSTS: 'blogpost',
    ADD_BLOG_POST: 'blogpost',
    UPDATE_BLOG_POST: 'blogpost',
    ADD_COMMENT: 'comment',
    UPDATE_COMMENT: 'comment',
    GET_ALL_BLOG_POST_BY_ID: (id: string) => `/blogpost/${id}`,
    DELETE_BLOG_POST_BY_ID: (id: string) => `/blogpost/${id}`,
    DELETE_COMMENT_BY_ID: (id: string) => `/comment/${id}`,
}