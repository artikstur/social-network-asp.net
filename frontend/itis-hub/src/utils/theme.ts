// theme.ts
export interface Theme {
    colors: {
        primary: string;
    };
    fonts: {
        logoSize: string;
        bodyFont: string;
    };
}

export const theme: Theme = {
    colors: {
        primary: '#007BFF',
    },
    fonts: {
        logoSize: '60px',
        bodyFont: '16px',
    },
};
